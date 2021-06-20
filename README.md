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
