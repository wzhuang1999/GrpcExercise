docker-compose down

docker volume rm pi_postgres-volume

docker-compose -f docker-compose.yml up --detach