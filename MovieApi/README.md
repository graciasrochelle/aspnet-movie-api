
# Movie REST API

> Build API provides access to a mocked movies database. [Project Requirements]( http://webjetapitest.azurewebsites.net/)
> There are 2 API operations available for 2 popular movie databases, cinemaworld and filmworld. To access these API you'll require an API token


```
/api/{cinemaworld or filmworld}/movies : This returns the movies that are available
/api/{cinemaworld or filmworld}/movie/{ID}: This returns the details of a single movie
```


---

![Watch the video](https://thumbs.gfycat.com/SecondhandArtisticFlea-size_restricted.gif)

---

## Installation

- [Visual Studio](https://visualstudio.microsoft.com/vs/mac/)
- [Postman](https://www.getpostman.com/)

### Clone

- Clone this repo to your local machine using `git clone https://rochellegracias@bitbucket.org/rochellegracias/ticket-management-system.git`

### Setup

- Install Visual Studio or VSCode
- Clone the project
- Open Cloned project in Visual Studio
- Restore NuGet Packages
- Clean and Build project
- Run project
- API should open in the browser

```shell
$ cd MovieApi
$ dotnet build
$ cd ..
$ dotnet test MovieApiTests/MovieApiTests.csproj
$ cd MovieApi
$ dotnet run MovieApi.sln
```

---

## Support

Reach out to me at [Linkedin](https://www.linkedin.com/in/graciasrochelle)
