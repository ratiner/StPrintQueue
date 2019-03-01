# StPrintQueue Home Assignment

A simple client/server queue manager.

Server side: ASP.NET Core 2.1 Rest API Server

Client side: Angular

## Missing things

1. Missing logging
2. If server goes down unexpectedly, queue is lost. Using a DB instead of json file is preferred.
3. Authentication is missing, anybody can mess up the queue.


## Assumptions

1. There is no delay between jobs. One job finishes, the second starts immidiately.
2. Job duration is in seconds and there is no duration less than 1 second. [Minimum job duration is 1s. Job duration is an integer]
3. Server is rock solid. Unexpected crash, power loss, loss of connectivity cannot happen.
4. Printer is ideal. Materials are unlimited and so on. In other words, there is nothing that can interrupt printing proccess except "Cancel" function of API.
5. Duration time is estimated perfrectly. There is no difference between duration entered in the UI and the actual printing time.
6. No parallel users adding to queue. In case of parallel users, the table will need a different refresh method or a fixed low polling delay.
7. Current refreshTimer (UI Table refresh) is set to the activeJob duration +1 second.  In other words, after each job estimated to complete +1 second, the table will refresh.
   The idea behind this: The only thing that can change without the user intervention is "active job removed by the server". And this can be predicted by the endTime of the job.
   I added one more second just to be sure the table will be refreshed "after" the server.
   With the assumptions [1] and [2] it should be OK for most cases.


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
Open a browser window at http://localhost:4200
   
## Deployment

Basically it is not part of the assignment. 
But a helpful README.md should have this section

## Built With

* [ASP.NET Core 2.1](https://www.asp.net/)
* [Angular](https://www.angular.io/)
* [Bootstrap](https://www.getbootstrap.com/)

## Authors

* **Roman Ratiner** - *Initial work*
