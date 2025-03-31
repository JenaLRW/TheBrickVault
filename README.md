

<div align="center">
<img src="https://github.com/user-attachments/assets/cad4e297-cfb3-40ff-934c-d63ddd30128a" width="" height="75" alt="line up of various LEGO minifigs" /><img src="https://github.com/user-attachments/assets/37936108-c2fe-47a1-afb7-d0027f2ba1e6" width="150" height="150" alt="Treasure chest full of colorful LEGO bricks" /><img src="https://github.com/user-attachments/assets/088cffbb-2afd-4438-bbe0-806204b3e425" width="" height="75" alt="line up of various LEGO minifigs" /></div>





# The Brick Vault



Find out what LEGO sets you can build with the pieces you already own!


## August 2024 Software Development Capstone Project
'The Brick Vault' is a Blazor Web App integrated with Rebrickable API to help LEGO builders manage and track their LEGO collection. The app allows them to search for LEGO sets, view a few details, and add those sets to their personal collection. The collection is stored in a database using Entity Framework Core and SQLite. User then can explore Rebrickable's collection of sets and determine which LEGO sets they can build using the parts they currently own.  The app features an user-friendly interface and asynchronous operations to ensure smooth interactions. 

Software Development Capstone Features List:
- After the user saves their Lego sets to the UI's table, a dictionary of Lego parts is created, where the key represents the unique InvPartId (inventory part ID) of each part, and the value is the quantity of that specific part the user owns. This dictionary provides a quick lookup of the user's Lego parts, allowing the program to efficiently determine whether the user has enough of a specific part to build a particular Lego set. It also facilitates matching the user's inventory to the parts required for sets, supporting features like identifying possible sets to build based on their available parts.
- The project is asynchronous in several ways, leveraging async/await functionality to manage operations that involve waiting for external resources, such as API calls or database queries, without blocking the rest of the program. For example: API calls in several methods using async HTTP requests and a neat small one, an intentional delay (```await Task.Delay(1100)```) to avoid hitting API rate limits.
- There are two related entities: DbLegoParts and RebrickableLegoSet. The DbLegoParts table stores information about the Lego parts a user has, while the RebrickableLegoSet table contains details about different Lego sets from the Rebrickable API. The function FindMatchingSetsAsync is responsible for finding sets that can be built using the parts the user has. It compares the user's parts with the parts required for each Lego set and returns a list of sets that can be constructed with the user's available pieces. This approach effectively combines data from both entities to identify matching Lego sets based on the user's inventory, providing a valuable feature for managing and utilizing their Lego collection.
- Implemented pagination for API requests to Rebrickable, allowing search results to load in batches of 10 sets per page. Users can request additional pages as needed, reducing the number of API calls and improving performance by only fetching more results when necessary.
- "The Brick Vault" implements CRUD operations by allowing users to CREATE Lego set entries by saving search results from the Rebrickable API to a local database. Users can READ saved sets by displaying them in the UI. The project does not support UPDATES since set details are read-only. However, users can DELETE sets from their collection, removing them from the database.


FYI, code contains a lot of comments. Without comments, I will get lost and unfortunately, I don't have very good memory recollection either.  Bear with me.

## Instructions

Firefox may give you some drama. Google handles it better. Enter at your own risk. 

Capstone Project was due on March 28th, 2025 at noon.  I have made some changes since then and as of today, March 31st, the new changes are not ready.  
- On this page at the top, please click on "### Commits", scroll down to March 28, 2025 and find the commit message "Added API Integration Challenges".
- To the right, there is a < > button, that will allow you to browse the repo at that commit.
- Click on the < >.
- Clone the app from there.  Thank you!

------------------------
### Download Git LFS (ONLY FOR COMMITS AFTER MARCH 28TH)
This project uses Git Large File Storage. Download Git LFS before cloning the repo and running the build. 

Windows:
- Download the Git LFS installer from [git-lfs.com](https://git-lfs.com/) and follow the prompts.
  
macOS:
- You can install Git LFS using Homebrew by running the following command in your terminal: ```brew install git-lfs```

After download, initalize Git LFS in the local repo, run the following command to initialize: ```git lfs install```

Clone the repo: ```git clone github.com/JenaLRW/TheBrickVault```
------------------------

### Run the build
- Open the ```.csproj``` file
- Run in terminal in the project's root folder: ```dotnet restore```
- Build the project: ```dotnet build```

### API Key is required.
- Retrieve the API key from the Program Director, Training Manager, or me on Slack or create a new account on Rebrickable.com and generate your own API key.

- Right click on the project's folder in Solution Explorer and select "Manage User Secrets".
- Add the API key to the secrets.json file in the following format:

secrets.json
```
{
  "Rebrickable:ApiKey": "the-api-key-here"  
}
```

OR
- Run in your CLI inside the project folder: ```dotnet user-secrets init```
- Add the API key manually using this command: ```dotnet user-secrets set "Rebrickable:ApiKey" "the-api-key-here"```

OR (Not recommended)
- Manually add the API key to SearchLegoSetsAsync and FetchPartsForSetsAsync methods in the RebrickableService.cs file. Replace the placeholder ```{_apiKey}``` with the actual API key.

### Test run data
- In the first search bar to look up Lego Sets
  - Enter "71043" (Hogwarts Castle - has quite a bit of parts)
  - Click on save.
  - Enter "Race Car"
  - Click on save on a set with more than 0 pieces.
- Click on Find Matching Sets.
  - If the database has a very high number of parts, the API potentially will be called too much and you will get a "Too many requests" failure. *I am working very diligently to find an optimization that will perform well with the app*



## Improvements Needed

- Unit tests
- Better API optimizations
- More IDE organization, more separate files for all of the classes and methods
- More defined search options
- Follow SOLID principles more closely
- Categorize sets to match user's parts by theme

## API Integration Challenges

During the development of this project, I initially aimed to integrate external API calls to retrieve data for certain functionality. However, I encountered performance limitations due to the sheer size of the API being used. The scale of the data proved to be too large for real-time API calls, which impacted the efficiency of my project.

To address this issue, I am in the process of importing the necessary data directly into the project and storing it in the database. This will allow me to eliminate the dependency on the external API for large data retrieval and improve overall performance while maintaining the required functionality.


# 

### Acknowledgements

Many many many thanks to Code:You and their staff and mentors for their guidance: Jenny Terry, August Mapp, Brian Luerman, Chris Metcalfe, Charles Kayser, and Michael Paddock. A few international friends as well. 


