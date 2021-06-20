# vaccine-slot-scanner project

This project uses Quarkus, the Supersonic Subatomic Java Framework.

If you want to learn more about Quarkus, please visit its website: https://quarkus.io/ .

## Running the application

Open terminal in the folder where you've downloaded the package reported in the Releases section of this repo.
Change the permissions to the file by executing `chmod +x ./vaccine-slot-scanner-1.0-SNAPSHOT-runner`.
You need to setup an environment variable to setup the email address that will receive the message: 
`export RECIPIENT=your_email@gmail.com` and finally you can run the application in the terminal itself by executing 
`./vaccine-slot-scanner-1.0-SNAPSHOT-runner`.

The project will run by printing logs every 5 seconds searching for empty slot; when the slot will be found an email 
will be sent to the proper environment variable set before executing.

## Packaging and running the application

The application can be packaged using:

```shell script
./mvnw package
```

It produces the `quarkus-run.jar` file in the `target/quarkus-app/` directory. Be aware that it’s not an _über-jar_ as
the dependencies are copied into the `target/quarkus-app/lib/` directory.

If you want to build an _über-jar_, execute the following command:

```shell script
./mvnw package -Dquarkus.package.type=uber-jar
```

The application is now runnable using `java -jar target/quarkus-app/quarkus-run.jar`.

## Creating a native executable

You can create a native executable using:

```shell script
./mvnw package -Pnative
```

Or, if you don't have GraalVM installed, you can run the native executable build in a container using:

```shell script
./mvnw package -Pnative -Dquarkus.native.container-build=true
```

You can then execute your native executable with: `./target/vaccine-slot-scanner-1.0-SNAPSHOT-runner`

If you want to learn more about building native executables, please consult https://quarkus.io/guides/maven-tooling.html
.

## Related guides


