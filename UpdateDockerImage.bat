docker build -f DataDash\Dockerfile . -t dockerregistry.sameplesite.com/datadashserver
docker build -f TestWebApi\Dockerfile . -t dockerregistry.sameplesite.com/datadashapi

REM docker image push dockerregistry.sameplesite.com/datadashserver
REM docker image push dockerregistry.sameplesite.com/datadashapi


@pause