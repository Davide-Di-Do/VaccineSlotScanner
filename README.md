# Vaccine Slot Scanner

## Running the application

Open terminal in the folder where you've downloaded the package reported in the Releases section of this repo.

The application needs some environment variables to run properly:
* NOTIFICATION_RECIPIENT: the email address that should receive the notification message
* MAILGUN_DOMAIN: domain of mailgun where the email are sent
* MAILGUN_APY_KEY: the API token to access to mail service

Now you can run the application itself `./VaccineSlotScanner`

The project will run by printing logs every 5 seconds searching for empty slot; when the slot will be found an email 
will be sent to the proper environment variable set before executing.

## Getting the app to run on the Pi. [Taken from official ASP.Net Core documentation]

* Install Linux on your Pi.
* Install the platform dependencies from your distro's package manager for .NET Core. .NET Core depends on some packages from the Linux package manager as prerequisites to running your application.

For Raspbian Debian 9 Jessie you need to do the following:
```
sudo apt-get update
sudo apt-get install curl libunwind8 gettext apt-transport-https
```
Copy your app, i.e. whole publish directory mentioned above, to the Raspberry Pi and execute run ./helloworld to see Hello World! from .NET Core running on your Pi! (make sure you chmod 755 ./helloworld)
