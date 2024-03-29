CREATE TABLE "users"
(
    "id"            SERIAL PRIMARY KEY,
    "username"      varchar UNIQUE NOT NULL,
    "password"      varchar        NOT NULL,
    "email"         varchar UNIQUE NOT NULL,
    "accessLevel"   int DEFAULT 0,
    "notifications" int,
    "status"        int DEFAULT 0,
    "imageId"       int,
    "dataMap"       varchar,
    "created"       varchar,
    "updated"       varchar
);

CREATE TABLE "athletes"
(
    "id"                   SERIAL PRIMARY KEY,
    "key"                  varchar,
    "name"                 varchar,
    "fullname"             varchar,
    "birthDate"            varchar,
    "birthPlace"           varchar,
    "citizenship"          varchar,
    "height"               float,
    "weight"               float,
    "position"             varchar,
    "positionKey"          int DEFAULT 0,
    "agent"                varchar,
    "currentInternational" varchar,
    "status"               varchar,
    "clubId"               int,
    "imageId"              int,
    "dataMap"              varchar,
    "created"              varchar,
    "updated"              varchar
);

CREATE TABLE "clubs"
(
    "id"       SERIAL PRIMARY KEY,
    "key"      varchar,
    "name"     varchar,
    "fullname" varchar,
    "country"  varchar,
    "founded"  varchar,
    "colors"   varchar,
    "members"  varchar,
    "stadium"  varchar,
    "address"  varchar,
    "homepage" varchar,
    "imageId"  int,
    "dataMap"  varchar,
    "created"  varchar,
    "updated"  varchar
);

CREATE TABLE "clubAthletes"
(
    "id"          SERIAL PRIMARY KEY,
    "clubId"      int,
    "athleteId"   int,
    "status"      int,
    "number"      int,
    "startDate"   varchar,
    "endDate"     varchar,
    "position"    varchar,
    "positionKey" int DEFAULT 0,
    "dataMap"     varchar,
    "created"     varchar,
    "updated"     varchar
);

CREATE TABLE "competitions"
(
    "id"      SERIAL PRIMARY KEY,
    "key"     varchar,
    "name"    varchar,
    "edition" varchar,
    "sportId" int,
    "imageId" int,
    "dataMap" varchar,
    "created" varchar,
    "updated" varchar
);

CREATE TABLE "competitionBoards"
(
    "id"              SERIAL PRIMARY KEY,
    "position"        int DEFAULT 0,
    "played"          int DEFAULT 0,
    "won"             int DEFAULT 0,
    "drawn"           int DEFAULT 0,
    "lost"            int DEFAULT 0,
    "goalsFor"        int DEFAULT 0,
    "goalsAgainst"    int DEFAULT 0,
    "goalsDifference" int DEFAULT 0,
    "points"          int DEFAULT 0,
    "clubId"          int,
    "competitionId"   int,
    "dataMap"         varchar,
    "created"         varchar,
    "updated"         varchar
);

CREATE TABLE "events"
(
    "id"          SERIAL PRIMARY KEY,
    "key"         varchar UNIQUE,
    "name"        varchar,
    "description" varchar,
    "sportId"     int,
    "imageId"     int,
    "dataMap"     varchar,
    "created"     varchar,
    "updated"     varchar
);

CREATE TABLE "gameEvents"
(
    "id"               SERIAL PRIMARY KEY,
    "key"              varchar UNIQUE,
    "time"             varchar,
    "gameTime"         varchar,
    "eventDescription" varchar,
    "gameId"           int,
    "eventId"          int,
    "athleteId"        int,
    "clubId"           int,
    "userId"           int,
    "dataMap"          varchar,
    "created"          varchar,
    "updated"          varchar
);

CREATE TABLE "gameAthletes"
(
    "id"        SERIAL PRIMARY KEY,
    "status"    int DEFAULT 0,
    "gameId"    int,
    "athleteId" int,
    "dataMap"   varchar,
    "created"   varchar,
    "updated"   varchar
);

CREATE TABLE "games"
(
    "id"                  SERIAL PRIMARY KEY,
    "timeStart"           varchar,
    "timeEnd"             varchar,
    "homeScore"           int DEFAULT 0,
    "visitorScore"        int DEFAULT 0,
    "homePenaltyScore"    int DEFAULT 0,
    "visitorPenaltyScore" int DEFAULT 0,
    "status"              int DEFAULT 0,
    "type"                varchar,
    "location"            varchar,
    "homeId"              int,
    "visitorId"           int,
    "competitionId"       int,
    "imageId"             int,
    "userId"              int,
    "dataMap"             varchar,
    "created"             varchar,
    "updated"             varchar
);

CREATE TABLE "gameUsers"
(
    "id"        SERIAL PRIMARY KEY,
    "userId"    int,
    "gameId"    int,
    "athleteId" int,
    "dataMap"   varchar,
    "created"   varchar,
    "updated"   varchar
);

CREATE TABLE "sports"
(
    "id"      SERIAL PRIMARY KEY,
    "name"    varchar UNIQUE,
    "imageId" int,
    "dataMap" varchar,
    "created" varchar,
    "updated" varchar
);

CREATE TABLE "images"
(
    "id"          SERIAL PRIMARY KEY,
    "imageUrl"    varchar UNIQUE,
    "tags"        varchar,
    "description" varchar,
    "dataMap"     varchar,
    "created"     varchar,
    "updated"     varchar
);

CREATE TABLE "favorites"
(
    "id"            SERIAL PRIMARY KEY,
    "userId"        int,
    "athleteId"     int,
    "clubId"        int,
    "competitionId" int,
    "gameId"        int,
    "dataMap"       varchar,
    "created"       varchar,
    "updated"       varchar
);

ALTER TABLE "users"
    ADD FOREIGN KEY ("imageId") REFERENCES "images" ("id");

ALTER TABLE "athletes"
    ADD FOREIGN KEY ("clubId") REFERENCES "clubs" ("id");

ALTER TABLE "athletes"
    ADD FOREIGN KEY ("imageId") REFERENCES "images" ("id");

ALTER TABLE "clubs"
    ADD FOREIGN KEY ("imageId") REFERENCES "images" ("id");

ALTER TABLE "competitions"
    ADD FOREIGN KEY ("sportId") REFERENCES "sports" ("id");

ALTER TABLE "competitions"
    ADD FOREIGN KEY ("imageId") REFERENCES "images" ("id");

ALTER TABLE "competitionBoards"
    ADD FOREIGN KEY ("clubId") REFERENCES "clubs" ("id");

ALTER TABLE "competitionBoards"
    ADD FOREIGN KEY ("competitionId") REFERENCES "competitions" ("id");

ALTER TABLE "events"
    ADD FOREIGN KEY ("sportId") REFERENCES "sports" ("id");

ALTER TABLE "events"
    ADD FOREIGN KEY ("imageId") REFERENCES "images" ("id");

ALTER TABLE "gameEvents"
    ADD FOREIGN KEY ("gameId") REFERENCES "games" ("id");

ALTER TABLE "gameEvents"
    ADD FOREIGN KEY ("eventId") REFERENCES "events" ("id");

ALTER TABLE "gameEvents"
    ADD FOREIGN KEY ("athleteId") REFERENCES "athletes" ("id");

ALTER TABLE "gameEvents"
    ADD FOREIGN KEY ("clubId") REFERENCES "clubs" ("id");

ALTER TABLE "gameEvents"
    ADD FOREIGN KEY ("userId") REFERENCES "users" ("id");

ALTER TABLE "gameAthletes"
    ADD FOREIGN KEY ("gameId") REFERENCES "games" ("id");

ALTER TABLE "gameAthletes"
    ADD FOREIGN KEY ("athleteId") REFERENCES "athletes" ("id");

ALTER TABLE "games"
    ADD FOREIGN KEY ("homeId") REFERENCES "clubs" ("id");

ALTER TABLE "games"
    ADD FOREIGN KEY ("visitorId") REFERENCES "clubs" ("id");

ALTER TABLE "games"
    ADD FOREIGN KEY ("competitionId") REFERENCES "competitions" ("id");

ALTER TABLE "games"
    ADD FOREIGN KEY ("imageId") REFERENCES "images" ("id");

ALTER TABLE "games"
    ADD FOREIGN KEY ("userId") REFERENCES "users" ("id");

ALTER TABLE "gameUsers"
    ADD FOREIGN KEY ("userId") REFERENCES "users" ("id");

ALTER TABLE "gameUsers"
    ADD FOREIGN KEY ("gameId") REFERENCES "games" ("id");

ALTER TABLE "gameUsers"
    ADD FOREIGN KEY ("athleteId") REFERENCES "athletes" ("id");

ALTER TABLE "sports"
    ADD FOREIGN KEY ("imageId") REFERENCES "images" ("id");

ALTER TABLE "favorites"
    ADD FOREIGN KEY ("userId") REFERENCES "users" ("id");

ALTER TABLE "favorites"
    ADD FOREIGN KEY ("athleteId") REFERENCES "athletes" ("id");

ALTER TABLE "favorites"
    ADD FOREIGN KEY ("clubId") REFERENCES "clubs" ("id");

ALTER TABLE "favorites"
    ADD FOREIGN KEY ("competitionId") REFERENCES "competitions" ("id");

ALTER TABLE "favorites"
    ADD FOREIGN KEY ("gameId") REFERENCES "games" ("id");

ALTER TABLE "clubAthletes"
    ADD FOREIGN KEY ("clubId") REFERENCES "clubs" ("id");

ALTER TABLE "clubAthletes"
    ADD FOREIGN KEY ("athleteId") REFERENCES "athletes" ("id");

COMMENT
ON COLUMN "gameEvents"."key" IS 'timestamp$userId';
