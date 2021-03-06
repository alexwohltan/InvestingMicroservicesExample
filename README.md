<div id="top"></div>
<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/github_username/repo_name">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">project_title</h3>

  <p align="center">
    project_description
    <br />
    <a href="https://github.com/github_username/repo_name"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/github_username/repo_name">View Demo</a>
    ·
    <a href="https://github.com/github_username/repo_name/issues">Report Bug</a>
    ·
    <a href="https://github.com/github_username/repo_name/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://example.com)

Here's a blank template to get started: To avoid retyping too much info. Do a search and replace with your text editor for the following: `github_username`, `repo_name`, `twitter_handle`, `linkedin_username`, `email_client`, `email`, `project_title`, `project_description`

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [Next.js](https://nextjs.org/)
* [React.js](https://reactjs.org/)
* [Vue.js](https://vuejs.org/)
* [Angular](https://angular.io/)
* [Svelte](https://svelte.dev/)
* [Laravel](https://laravel.com)
* [Bootstrap](https://getbootstrap.com)
* [JQuery](https://jquery.com)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

We need docker to run containerized applications. 

For Mac: 
1. Download Docker, extract it, and drag it into your Application folder. https://docs.docker.com/desktop/mac/install/
2. Launch Docker, and go to Preferences > Advanced and increase its memory allocation to 4GB

We also need a SQL Server. We could use a remote SQL server but for development it is easier to just use the sql docker image.

Pull the image
```sh
docker pull mcr.microsoft.com/azure-sql-edge
```

Install and configure our SQL Server
```sh
docker run -d --name 'InvestingMicroservicesSQLServer' -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=EukalyptusHantel35!' -p 1433:1433 mcr.microsoft.com/azure-sql-edge
```

We also need the dotnet ef tool.
```sh
dotnet tool install --global dotnet-ef
```

For asynchronous messaging between the services we need an EventBus -> RabbitMQ.
```sh
docker pull rabbitmq
docker run -d --name RabbitMQ -p 5672:5672 -p 15672:15672 rabbitmq
```
Open the RabbitMQ CLI in docker and execute this command to enable the GUI.
```sh
rabbitmq-plugins enable rabbitmq_management
```

Go to http://localhost:15672 and add our user. (user: alexander, pass: EukalyptusHantel35!)

Install Azura Data Studio so we can see what happenes in our database. https://docs.microsoft.com/en-gb/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15

### Configure Databases

Locate the FundamentalData Project.
```sh
cd src/FundamentalData
```

Create the Entity Framework Migrations.
```sh
dotnet ef migrations add InitialCommit
```

Apply the migrations to the database.
```sh
dotnet ef databse update
```

Now the same for the Benchmark database
Locate the Benchmark Project.
```sh
cd src/Benchmark/Benchmark
```

Create the Entity Framework Migrations.
```sh
dotnet ef migrations add InitialCommit
```

Apply the migrations to the database.
```sh
dotnet ef databse update
```

## Usage

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [ ] Feature 1
- [ ] Feature 2
- [ ] Feature 3
    - [ ] Nested Feature

See the [open issues](https://github.com/github_username/repo_name/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Alexander - [@twitter_handle](https://twitter.com/alexwohltan) - alexander@wohltan.at

Project Link: [https://github.com/alexwohltan/InvestingMicroservicesExample](https://github.com/alexwohltan/InvestingMicroservicesExample)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* []()
* []()
* []()

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/alexwohltan/InvestingMicroservicesExample.svg?style=for-the-badge
[contributors-url]: https://github.com/alexwohltan/InvestingMicroservicesExample/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/alexwohltan/InvestingMicroservicesExample.svg?style=for-the-badge
[forks-url]: https://github.com/alexwohltan/InvestingMicroservicesExample/network/members
[stars-shield]: https://img.shields.io/github/stars/alexwohltan/InvestingMicroservicesExample.svg?style=for-the-badge
[stars-url]: https://github.com/alexwohltan/InvestingMicroservicesExample/stargazers
[issues-shield]: https://img.shields.io/github/issues/alexwohltan/InvestingMicroservicesExample.svg?style=for-the-badge
[issues-url]: https://github.com/alexwohltan/InvestingMicroservicesExample/issues
[license-shield]: https://img.shields.io/github/license/alexwohltan/InvestingMicroservicesExample.svg?style=for-the-badge
[license-url]: https://github.com/alexwohltan/InvestingMicroservicesExample/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/alexanderwohltan
[product-screenshot]: images/screenshot.png
