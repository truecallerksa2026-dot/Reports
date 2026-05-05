#!/bin/bash

if [[ ! -d certs ]]
then
    mkdir certs
    cd certs/
    if [[ ! -f localhost.pfx ]]
    then
        dotnet dev-certs https -v -ep localhost.pfx -p 2272ee95-295e-409c-adf8-b2d5842ac7e7 -t
    fi
    cd ../
fi

docker-compose up -d
