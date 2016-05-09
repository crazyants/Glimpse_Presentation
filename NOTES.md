Project
 * extracted from a real application
 * non-trivial example application
 * uses modern tools and techniques

Stack
 - Server
 * ASP.NET MVC 5 
 * Entity Framework 6
 * Autofac
 * NLog
 * Mediatr
 - Client
 * Bootstrap
 * Bootswatch
 * jQuery

Pattern
 * Command/Query Segregation
 * Notification Publishing
 * Database Interception
 

DEMO: 
 - Fire up app
 - Show Orgaizations, explain the relationship between Organization and Features
 - Demonstrate enabling/disabling a feature
 - Point out the extreme lag in disabling features

Discussion: How would you go about identifying the issue?


Install-Package Glimpse.Mvc5
 * Install package
 * Show modified files
   * web.config
   * policy file

DEMO: 
 - Fire up app
 - navigate to glimpse.asxd
 - enable the HUD
 - demonstrate the HUD
   - Server Side
   - Client Side
   - Ajax Calls
 - Reproduce bug and locate source of delay


Install-Package Glimpse.EF6
 * Install package

DEMO:
 - Fire up the app
 - navigate to organizations
 - show queries
 - update an organization, show history with the update
 - mention ADO package
 - mention that you cannot at this time use both


Install-Package Glimpse.NLog
 * Show logs folder, wouldnt it be nice to have more immediate access per request?
 * Install package

DEMO: 
 - Fire up the app
 - navigate to organizations
 - show logs, with filtering



Install-Package Glimpse.Autofac
 - Mention the container and lifetime scope
 - Can cause really hard to diagnose bugs
 - Install package

DEMO:
 - Fire up the app
 - Show the new autofac tab, but it is empty
 - Add the following lines to ContainerConfig.cs 
    //Add 3rd party integrations that do not 
    //supply autofac modules
    container.ActivateGlimpse();
 - Fire up the app
 - show the autofac tab with data