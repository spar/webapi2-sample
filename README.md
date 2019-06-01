# Introduction
This is sample Web Api 2 project. It exposes basic CRUD operations endpoints for Product entity.

# Authentication and Authorization
Auth0 has been used for Authentication and Authorization. It provides a universal authentication & authorization platform for web, mobile and legacy applications. There are  easy to integrate SDKS for most of the modern frameworks.

Project uses Free trier of Auth0. Free tier comes with:
1. 7000 free active users and unlimited logins
2. Passwordless
3. Lock widget for web, iOS & Android
4. Up to 2 social identity providers
5. Unlimited serverless rules
6. Community Support

Another option was to use Aps.net Identity but it requires a SQL server which we would have to manage. Delegating authentication and authorization to Auth0 made sense. There was less confiuration in code and one less resource to manage.

# Running Locally
1. Checkout the repo locally from GitHub
2. Open `Product.sln` in Visual Studio
3. Set `Product.Api` and `Product.Ui` as startup projects. You can do this by going into properties of the solutions and select those two projects as startup projects.
4. Update `Auth0Domain` and `Auth0ApiIdentifier` in web.config of `Product.Api with relevant entries. Or set `AuthenticationEnabled` to `false` to test without authentication.
5. Update `AUTH0_CLIENT_ID`, `AUTH0_DOMAIN`, `AUTH0_AUDIENCE`, `API_HOST` in variables.js of `Product.Ui` with relevant entries. Or set `AUTHENTICATION_ENABLED` to `false` to test without authentication.

Note: Please ask me if you need Auth0 tenant information for testing locally with authentication and authorizaration. Otherwise you can test with a live version.

# Live Version
* API: https://productwebapi.azurewebsites.net/api/products 
* UI: https://productui.azurewebsites.net

Once you press login in UI , it will take you to Auth0 hosted login page, once successfully logged in, you will be redirect back to UI.

# User Interface
UI is a vanilla SPA without any JavaScript framework. Bootstrap is used for responsive design. 

Currently, only querying exisiting products is supported. If the search box is empty, it will return the first 50 products otherwise all the products containing search text will return. However, API supports CRUD operations. They can be tested with Postman.

# Api
There are four scopes- read:products, create:products, update:products, delete:products defined for authorization. These scopes are defined as permissions in Auth0 and assigned to two roles: Admin and Product Editor. These two roles are assigned to users. When a user logs in, based on their role, relevant scopes are added to access token.