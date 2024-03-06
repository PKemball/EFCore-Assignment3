
using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.QueryModels;
using Infrastructure.DataRandomizer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections;
using Microsoft.VisualBasic;

Console.ForegroundColor = ConsoleColor.Magenta;

//Check that the entities have the proper properties
//Make sure project references are correct
//Create Migration and update the database -> make sure you run your commands against the right project in the solution

//Create a new instance of the data context
//Create a new instance of DataCreator and pass your email into the constructor
//Create a variable and populate it with output of the DataCreator GetData method
//Save the Users, BlogTypes, PostTypes, Blogs, and Posts that are in the output of GetData to the data context and Save the Changes

//In this assignment, you need to return all of the blogs and the associated posts belonging to each blog. 
//Also, and most important, you need to display supporting data names and statuses.
//For instance, in the Blog Entity, we use a foreign key to point to the Blog type.
//Therefore, you will need to include the Blog type and return the BlogType.Name property - not the foreign key value.This is also true to Post types and statuses.

//There are many ways to produce the results and, therefore, multiple solutions will be acceptable.
//There are, of course, efficient ways and not-so-efficient ways but this assignment is primarily focused on the end result.

//Once the query is completed and working correctly, output the results of the query to the screen.
//Below is a minimum list of the data fields that must be returned:

//Blog URL
//Blog IsPublic
//Blog Type Name
//Post User Name
//Post User Email Address
//The total number of blog posts the User has posted
//Post Type Name
//Exclude any post where the Post Type is not Active.

//Exclude any post where the Blog Type is not Active.

//Finally, sort the output using the user name - in alphabetical order(A to Z).

//---------------------------------------------------------------------------------------------------------

//var context = new DataContext();
//var dataRandomizer = new DataCreator("kemballp");
//var data = dataRandomizer.GetData();
//context.Users.AddRange(data.Users);
//context.Blogs.AddRange(data.Blogs);
//context.Posts.AddRange(data.Posts);
//context.BlogTypes.AddRange(data.BlogTypes);
//context.PostTypes.AddRange(data.PostTypes);
//context.SaveChanges();


//--------------------------------------------------------------

// Instructions make it sound like all blogs should be included so that's what I did.
// Only Posts related to Active PostType.Status and BlogType.Status are included. 

// I get the feeling about half or more of what I did here was unnecessary...

// My models are in  "Infrastructure/QueryModels" folder (where should I have put them?)

//My biggest question is if Include() and ThenInclude() would have helped me generate this output in a simpler way.
//I noticed that Select() removes the need for them, at least in my specific case. 
//I'd love to see how you would do this, if you could send me your "best solution" on slack I'd greatly appreciate it  

List<QueryResultModel> Query(DataContext context)
{
    var query = context.Blogs
   .Select(blog => new QueryResultModel //Created a model to structure the result output (I didn't like using an anonymous type)
   {
       BlogURL = blog.Url,
       BlogIsPublic = blog.IsPublic,
       BlogTypeName = blog.BlogType.Name,
       BlogTypeStatus = blog.BlogType.Status, //Not required for assignment but made output easier to verify
       Posts = blog.Posts
       .Where(post => post.PostType.Status == Status.Active && post.Blog.BlogType.Status == Status.Active) // excludes posts without both an active BlogType and PostType
            .OrderBy(post => post.User.Name) //Orders the Posts within the Blog from A to Z
                .Select(post => new PostQueryModel // Okay... Maybe I got carried away with the models 
                    {
                        PostTypeName = post.PostType.Name,
                        PostTypeStatus = post.PostType.Status, //Not required for assignment but made output easier to verify
                        UserName = post.User.Name,
                        UserEmail = post.User.EmailAddress,
                        UserPostsCount = post.User.Posts.Count
                    }).ToList()
   })
   .OrderBy(blog => blog.Posts
        .OrderBy(post => post.UserName)
            .Select(post => post.UserName) //Orders the Blogs by the First UserName (which is already ordered above)
                .FirstOrDefault() ?? "zzz") //Added default "zzz" to order blogs without posts fitting the criteria to the end of the display.
     .ToList();
    return query;
}

void DisplayQueryResults(List<QueryResultModel> query)
{ //Display results from Query() Method.
    int count = 1;
    query.ForEach(item =>
    {
        Console.WriteLine($"Blog #{count}:");
        var properties = item.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType.IsGenericType) //Checks if property is a list
            {
                

                var listValue = property.GetValue(item);
                if (listValue != null) //checks for null value
                {
                    int postCount = 1;
                    foreach (var o in (IEnumerable)listValue) //displays each property of each object within the list
                    {
                        Console.WriteLine($"Post #{postCount}:");
                        var objectProperties = o.GetType().GetProperties();
                        foreach(var prop in objectProperties)
                        {
                            var val = prop.GetValue(o);
                            Console.WriteLine($"{prop.Name}: {val}");
                        }
                        postCount += 1;
                    }
                }
            }
            else { //displays the property if the type is not a list
                var value = property.GetValue(item);
                Console.WriteLine($"{property.Name}: {value}");
            }
        }
        Console.WriteLine();
        count += 1;
    });
}

var context = new DataContext(); //I have 94 blogs in my data pool.
var queryResult = Query(context); //query the data
DisplayQueryResults(queryResult); //display the query to the console

Console.ReadLine();
