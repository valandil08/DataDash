docker build -f DataDash\Dockerfile . -t dockerregistry.runeclawgames.com/datadashserver
docker build -f TestWebApi\Dockerfile . -t dockerregistry.runeclawgames.com/datadashapi

docker image push dockerregistry.runeclawgames.com/datadashserver
docker image push dockerregistry.runeclawgames.com/datadashapi


@pause