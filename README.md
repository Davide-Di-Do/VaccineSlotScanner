# Vaccine Slot Scanner

## Running the application

Open terminal in the folder where you've downloaded the package reported in the Releases section of this repo.

The application needs some environment variables to run properly:
* NOTIFICATION_RECIPIENT: the email address that should receive the notification message
* MAILGUN_DOMAIN: domain of mailgun where the email are sent
* MAILGUN_API_KEY: the API token to access to mail service

Now you can run the application itself `./VaccineSlotScanner`

The project will run by printing logs every 1 hour searching for empty slots; when a slot is found,
the program will send an email to the recipient set in the environment variable.

## Getting started to scan. [Taken from official ASP.Net Core documentation]
Before installing the framework you need to add some dependencies:
```
sudo apt-get update
sudo apt-get install curl libunwind8 gettext apt-transport-https
```

The worker is based on the latest version of the framework .Net Core 6.0 
you can download the pre-compiled package for RaspberryPi on this link:
https://download.visualstudio.microsoft.com/download/pr/a7e77f1a-db9d-403f-a611-f925cea0e6f3/af5baacfa05d023671f08bf14f98bcb2/aspnetcore-runtime-6.0.2-linux-arm.tar.gz

Then extract the archive where you prefer on your system, so export environment variables
to allow your system to recognize the where the stack is installed:
```
export DOTNET_ROOT=[root_folder_where_archive_was_extracted]
export PATH=$PATH:[root_folder_where_archive_was_extracted]
```

To ensure that the stack is deployed as expected run the command `dotnet --version` and double
check that the output matches with the version 6.x.x.

Extract downloaded archive from github and extract anywhere on your system, change permission to the 
executable if necessary (should have 755 permission level). 
Now you are redy to run the scanner with the command `./VaccineSlotScanner`
