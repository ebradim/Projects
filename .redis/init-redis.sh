#!/bin/bash
set -e

# Set Redis password
echo "requirepass Test@123" >> /usr/local/etc/redis/redis.conf
