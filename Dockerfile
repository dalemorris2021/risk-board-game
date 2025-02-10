FROM ubuntu:latest
LABEL authors="xavierb" version="1.0"

RUN ./build.sh && ./run.sh

ENTRYPOINT ["top", "-b"]