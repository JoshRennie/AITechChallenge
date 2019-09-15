# AITechChallenge

Tech:

Website:
	- React
	- Redux 
	- Built using create-react-app

API:
	-ASP.NetCore WebApi

TESTS:
	-NUnit


Decisions/Tradeoffs:

The data for the app is all stored in static lists within the respective DAOs. Usually this would be stored in a DB and accessed from a DAO.

The solution is split out into seperate projects to easily show the different tiers of the application.

Since the website will be calling the API, and that API would be publically exposed, I added basic authentication to the API. In a real-life scenario, this would be a more robust security such as OAuth2.

I've used a base DAO/service interface that takes a generic type argument, which the DAOs/services then implement. This means that if more DAOs need to be added in the future that also need to implement these methods (i.e car models) then setting up a new DAO/service is easy

All DAOs/services are registered at startup and passed using dependancy injection. They're created as singletons as there's no ned for more than one of each.

Since the items are stored in a "DB" rather than as properites against the job, there's no way of telling what validation need to be performed against them. So I added custom validation fields (MaxQuantity, related items, allowed quantities) that allow the validation to work without having to harcode item names.

Labour cost would probally be stored in the DB against a shop of some sort in the real world, but to save time, I've kept it in the app settings. I created a static class for appsettings instead of using dependancy injection as the settings need to be accessed from class libraries and it saves time to just store in a static class as startup.
