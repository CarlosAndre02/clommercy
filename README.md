# Clommercy
WIP...

The main goal of this project is to study. I documented the key topics studied and developed in `Studying.md`

## How to test
## Prerequisite
## Technologies
## Tree structure
## How to run

### Setup docker compose
For now, i'm not able to run initial database script automatically. To run manually, first initiate the database container:
```bash
docker-compose up -d
``` 
Get the container ID with:
```bash
docker ps
```
Then, enter the container with:
```bash
docker exec -it <container ID> "bash"
```
Run the following to run the initial script mannualy:
```bash
/opt/mssql-tools18/bin/sqlcmd -C -S localhost -l 60 -U SA -P ClommercySqlPass123! -d master -i /docker-entrypoint-initdb.d/init-db.sql
```
