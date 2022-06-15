# ASP.NET Core with AutoWrapper 

## What is AutoWrapper

[AutoWrapper](https://vmsdurano.com/asp-net-core-with-autowrapper-customizing-the-default-response-output/) is a simple customizable global exception handler and response wrapper for ASP.NET Core APIs.It uses an middleware to intercept incoming HTTP requests and automatically wraps the responses by providing a consistent response format for both successful and error results. 

 
## About this exercise

Previously we scaffolded a new Angular application in which we have integrated

* Scaffolded the angular application
* FontAwesome Library for icons
* Bootstrap Library for styling buttons
* Bootstrap NavBar component
* We have multiple components e.g. (CreateAccountComponent,   ManageAccountsComponent, DepositFundsComponent, TransferFundsComponent) in our application for which we have already configured routing.
* SideNav having links which are navigating to these components.
* We developed a base structure of an api solution in Asp.net core that have just two api functions GetLast12MonthBalances & GetLast12MonthBalances/{userId} which returns data of the last 12 months total balances.
* There is an authorization service with two functions Login & Logout, The login function is setting up a hardcoded user properties (Name,Email,Roles) and storing it in local storage where as logout function is removing that user object from local storage.
* Links on the sideNav are shown or hidden based on the logged in user's role
*  We also have a toolbar that shows Logged in User's Name.



## In this exercise

 * We will implement AutoWrapper in BBankAPI project. 
 * We will handle uniform response in Angular.


 Here are the steps to begin with 

# Server Side Implementation 

## Step 1 : Install AutoWrapper.Core Library

To start the AutoWrapper we will first install the AutoWrapper.Core libraries using command as below :
```
Install-Package AutoWrapper.Core -Version 2.0.1
```


##  Step 2 : Register the AutoWrapper middleware

We will create  `UseApiResponseAndExceptionWrapper` in `program.cs` file to register the middleware as below :

```cs
app.UseApiResponseAndExceptionWrapper();
```


##  Step 3 : Change Action response

To implement the uniform response we will change the `Task<ActionResult>` return type to `Task<ApiResponse>` in `TransactionController` Actions.

In case of successful response we will return  `ApiResponse` class object whereas in case of error we will return `ApiException` class  object.

Here is the code as below : 

```cs
  public async Task<ApiResponse> GetLast12MonthBalances()
  {
      try
      {
          logger.LogInformation("Executing GetLast12MonthBalances");
          var res = await _transactionService.GetLast12MonthBalances(null);
          return new ApiResponse("Last 12 Month Balances Returned.", res);
      }
      catch (Exception ex)
       {
                throw new ApiException(ex);
       }
  }
```

That's it on the server side implementation, Run the API and see its working as below :

![APIRunning](https://user-images.githubusercontent.com/100709775/173840689-e121bb7c-4464-4249-8984-fe1d317f63fe.PNG)


# Client Side Implementation 

##  Step 1 : Implement Uniform ApiResponse

We will create a new folder `models` under app folder and then create a new file named `api-Response.ts` in which we will create an interface `ApiResponse` which will be used as a uniform response receiver from API. The `ApiResponse` will be inheriting `ResponseException` interface and this interface will be inheriting array of `ValidationError` interface as below :

```ts
export interface ApiResponse {
    isError: boolean;
    message: string;
    responseException: ResponseException;
}
export interface ResponseException {
    exceptionMessage: string;
    validationErrors: ValidationError[]
}

export interface ValidationError {
    name: string;
    reason: string
}
```

## Step 2 : Implement Line Graph Data Response

Go to `line-graph-data.ts` file and here We will create a new interface `LineGraphDataResponse` which will be extending  `ApiResponse` interface. We will use LineGraphDataResponse class for receiving uniform response specifically for `LineGraphData`.

## Step 3 : Update TransactionService class

We will be replacing `LineGraphData` with `LineGraphDataResponse` in getLast12MonthBalances returned observable type as well as in `httpClient` get request as below :

```ts
  getLast12MonthBalances(accountId?: string): Observable<LineGraphDataResponse> {
    if (accountId === null) {
      return this.httpClient.get<LineGraphDataResponse>(`${environment.apiUrlBase}Transaction/GetLast12MonthBalances`);
    }
    return this.httpClient.get<LineGraphDataResponse>(`${environment.apiUrlBase}Transaction/GetLast12MonthBalances/${accountId}`);
  }
```

## Step 4 : Update DashboardComponent

Go to `DashboardComponent.ts` and change lineGraphData  response received  `data.result` as below :

```ts
this.transactionService
      .getLast12MonthBalances('37846734-172e-4149-8cec-6f43d1eb3f60')
      .subscribe({
        next: (data) => {
          this.lineGraphData = data.result; // received response is in data.result object.
        },
        error: (error) => {
          console.log(error.responseException.exceptionMessage);
        },
      });
```


Run the project and see its working.
