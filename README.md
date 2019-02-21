# StPrintQueue Home Assignment

A simple client/server queue manager.

Server side: ASP.NET Core 2.1 Rest API Server

Client side: Angular

## Getting Started

Clone the whole solution from git
```
git clone https://github.com/ratiner/StPrintQueue.git
```

The solution is divided into 3 folders:
1. StPrintQueue: Web Manager powered by Angular Cli.
2. StPrintQueue.Api: REST Api Server, powered by ASP.NET Core 2.1
3. StPrintQueue.Db: Dependency of REST Api Server.

### Prerequisites

NodeJS
Angular CLI
ASP.NET Core 2.1
Visual Studio 2017 (optional)

### Installing

After cloning the whole repository,
1. Build and run the REST API project
opening solution file in visual studio and running the Rest API project (F5)
```
StPrintQueue.sln
```


2. Step into the angular project
```
cd StPrintQueue/StPrintQueue
```

3. Install all node dependencies
```
npm install
```
4. Run angular cli project
```
npm start
```
Open a browser window at http://localhost:3000
   
## Deployment

Basically it is not part of the assignment. 
But a helpful README.md should have this section

## Built With

* [ASP.NET Core 2.1](https://www.asp.net/)
* [Angular](https://www.angular.io/)
* [Bootstrap](https://www.getbootstrap.com/)

## Authors

* **Roman Ratiner** - *Initial work*