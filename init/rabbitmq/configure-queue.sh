#!/bin/bash

HOST="rabbitmq"
USER="guest"
PASS="guest"
API="http://$HOST:15672/api"

EXCHANGE="labs-challenge-api.exchange"
QUEUE="labs-challenge-api-email.confirmation"
ROUTING_KEY="labs-challenge-api-email.confirmation"

echo "Waiting for RabbitMQ to become available..."

# Wait until RabbitMQ management API is reachable and node is healthy
until curl -s -u $USER:$PASS "$API/healthchecks/node" | grep -q "\"status\":\"ok\""; do
    >&2 echo "RabbitMQ is unavailable - waiting..."
    sleep 2
done

echo "RabbitMQ is up and running. Creating Exchange, Queue, and Binding..."

# Create Exchange
curl -i -u $USER:$PASS -H "content-type:application/json" \
-XPUT -d'{"type":"direct","durable":true}' \
$API/exchanges/%2f/$EXCHANGE

# Create Queue
curl -i -u $USER:$PASS -H "content-type:application/json" \
-XPUT -d'{"durable":true}' \
$API/queues/%2f/$QUEUE

# Bind Queue to Exchange with Routing Key
curl -i -u $USER:$PASS -H "content-type:application/json" \
-XPOST -d'{"routing_key":"'"$ROUTING_KEY"'","arguments":{}}' \
$API/bindings/%2f/e/$EXCHANGE/q/$QUEUE

echo "Exchange and Queue successfully created"
