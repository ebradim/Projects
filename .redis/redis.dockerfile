FROM redis:7.4.2

# Create the directory for the Redis configuration file
RUN mkdir -p /usr/local/etc/redis

# Copy the shell script into the Docker image
COPY init-redis.sh /usr/local/bin/init-redis.sh

# Make the shell script executable
RUN chmod +x /usr/local/bin/init-redis.sh

# Run the shell script
RUN /usr/local/bin/init-redis.sh

# Expose the Redis port
EXPOSE 6379

# Use the default command provided by the Redis image
CMD ["redis-server", "/usr/local/etc/redis/redis.conf"]
