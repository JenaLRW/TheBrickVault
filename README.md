

<div align="center">
<img src="https://github.com/user-attachments/assets/cad4e297-cfb3-40ff-934c-d63ddd30128a" width="" height="100" alt="line up of various LEGO minifigs" /><img src="https://github.com/user-attachments/assets/37936108-c2fe-47a1-afb7-d0027f2ba1e6" width="200" height="200" alt="Treasure chest full of colorful LEGO bricks" /><img src="https://github.com/user-attachments/assets/088cffbb-2afd-4438-bbe0-806204b3e425" width="" height="100" alt="line up of various LEGO minifigs" /></div>





# The Brick Vault



Find out what LEGO sets you can build with the pieces you already own!


## August 2024 Software Development Capstone Project
'The Brick Vault' is a Blazor Web App integrated with Rebrickable API to help LEGO builders manage and track their LEGO collection.  The app allows them to search for LEGO sets, view a few details, and add those sets to their personal collection. The collection is stored in a database using Entity Framework Core and SQLite. User then can explore Rebrickable's collection of sets and determine which LEGO sets they can build using the parts they currently own.  The app features an user-friendly interface and asynchronous operations to ensure smooth interactions. 

Software Development Capstone Features List:
- After the user saves their Lego sets to the UI's table, a dictionary of Lego parts is created, where the key represents the unique InvPartId (inventory part ID) of each part, and the value is the quantity of that specific part the user owns. This dictionary provides a quick lookup of the user's Lego parts, allowing the program to efficiently determine whether the user has enough of a specific part to build a particular Lego set. It also facilitates matching the user's inventory to the parts required for sets, supporting features like identifying possible sets to build based on their available parts.
- The project is asynchronous in several ways, leveraging async/await functionality to manage operations that involve waiting for external resources, such as API calls or database queries, without blocking the rest of the program. For example: API calls in several methods using async HTTP requests and a neat small one, an intentional delay (```await Task.Delay(1220)```) to avoid hitting API rate limits.
- There are two related entities: DbLegoParts and RebrickableLegoSet. The DbLegoParts table stores information about the Lego parts a user has, while the RebrickableLegoSet table contains details about different Lego sets from the Rebrickable API. The function FindMatchingSetsAsync is responsible for finding sets that can be built using the parts the user has. It compares the user's parts with the parts required for each Lego set and returns a list of sets that can be constructed with the user's available pieces. This approach effectively combines data from both entities to identify matching Lego sets based on the user's inventory, providing a valuable feature for managing and utilizing their Lego collection.

FYI, code contains a lot of comments.  Without comments, I will get lost and unfortunately, I don't have very good memory recollection either.  Bear with me.

## Instructions

### API Key is required.
- Retrieve the API key from the director or me on Slack or create a new account on Rebrickable.com and generate your own API key.

- Right click on the project's folder and select "Manage User Secrets".
- Add the API key to the secrets.json file in the following format:

secrets.json
```
{
  "Rebrickable:ApiKey": "the-api-key-here"  
}
```
OR
- Run in your CLI inside the project folder ```dotnet user-secrets init```
- Add the API key manually using this command: ```dotnet user-secrets set "Rebrickable:ApiKey" "the-api-key-here"```

OR
- Manually add the API key to SearchLegoSetsAsync and FetchPartsForSetsAsync methods in the RebrickableService.cs file. Replace the placeholder ```{_apiKey}``` with the actual API key.

Many many many thanks to Code:You and their mentors for their guidance: Chris Metcalfe, Charles Kayser, and Michael Paddock.


