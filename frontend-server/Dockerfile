FROM node:19-alpine

RUN mkdir node
COPY . ./node
WORKDIR /node/client

RUN npm install 

RUN npm run build

WORKDIR /node

RUN npm install

EXPOSE 81

CMD node main.js