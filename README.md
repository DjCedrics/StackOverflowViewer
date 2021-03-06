# Stack Overflow Viewer Application

Stack Overflow Viewer Application (SOVA) is a single-page application that allows users browse posts in Stack Overflow, display various stats and mark the posts that they want to save for later. The application is based on ASP.NET and runs its own API instead of directly accessing StackExchange API. To avoid excessive storage usage, the database includes a small portion of the posts (~100.000). 

## Progress

Completed.

## Technologies that are used

- MySQL
- C#
- ASP.Net
- Entity Framework
- LINQ
- Bootstrap
- Knockout.JS

## Notes for contributors

You need to create a table called `MarkedPosts` in order to use the latest functionalities. SQL code is provided below.

```mysql
CREATE TABLE `markedposts` (
  `id` int(11) DEFAULT NULL,
  `notes` varchar(256) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
```

Body template for `CREATE, DELETE, UPDATE` operations that are used in marking posts:

```json
{
	"id" : 12345678,
	"notes": "Add some notes to your marked post"
}
```


And, don't forget to specify the content type while making calls.

```http
Content-Type: application/json
```
