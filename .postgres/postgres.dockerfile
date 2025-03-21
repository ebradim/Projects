FROM postgres:17.4

# Install the required extensions
RUN apt-get update && apt-get install -y postgresql-contrib postgis

# Copy the initialization script to the Docker entrypoint directory
COPY init-db.sh /docker-entrypoint-initdb.d/
