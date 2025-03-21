# The Brick Vault

FYI, code contains a lot of comments.  This is my first major project and without comments, I will get lost and unfortunately, I don't have very good memory recollection either.  Bear with me. 

API Key is required.
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

- Manually add the API key to SearchLegoSetsAsync and FetchPartsForSetsAsync methods in the RebrickableService.cs file. Replace the placeholder {_apiKey} with the actual API key._}

