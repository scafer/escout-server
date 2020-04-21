﻿namespace escoutTests.Resources
{
    public static class Queries
    {
        public const string CreateDatabase =
            "DROP SCHEMA public CASCADE;\r\nCREATE SCHEMA public;\r\nGRANT ALL ON SCHEMA public TO postgres;\r\nGRANT ALL ON SCHEMA public TO PUBLIC;\r\n\r\nCREATE TABLE \"users\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"username\" varchar UNIQUE NOT NULL,\r\n  \"password\" varchar NOT NULL,\r\n  \"email\" varchar UNIQUE NOT NULL,\r\n  \"accessLevel\" int DEFAULT 0,\r\n  \"notifications\" int,\r\n  \"status\" int,\r\n  \"imageId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"athletes\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"key\" varchar,\r\n  \"name\" varchar,\r\n  \"fullname\" varchar,\r\n  \"birthDate\" varchar,\r\n  \"birthPlace\" varchar,\r\n  \"citizenship\" varchar,\r\n  \"height\" float,\r\n  \"weight\" float,\r\n  \"position\" varchar,\r\n  \"agent\" varchar,\r\n  \"currentInternational\" varchar,\r\n  \"status\" varchar,\r\n  \"clubId\" int,\r\n  \"imageId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"clubs\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"key\" varchar,\r\n  \"name\" varchar,\r\n  \"fullname\" varchar,\r\n  \"country\" varchar,\r\n  \"founded\" varchar,\r\n  \"colors\" varchar,\r\n  \"members\" varchar,\r\n  \"stadium\" varchar,\r\n  \"address\" varchar,\r\n  \"homepage\" varchar,\r\n  \"imageId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"competitions\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"key\" varchar,\r\n  \"name\" varchar,\r\n  \"edition\" varchar,\r\n  \"sportId\" int,\r\n  \"imageId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"competitionBoards\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"position\" int,\r\n  \"played\" int,\r\n  \"won\" int,\r\n  \"drawn\" int,\r\n  \"lost\" int,\r\n  \"goalsFor\" int,\r\n  \"goalsAgainst\" int,\r\n  \"goalsDifference\" int,\r\n  \"points\" int,\r\n  \"clubId\" int,\r\n  \"competitionId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"events\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"name\" varchar,\r\n  \"description\" varchar,\r\n  \"sportId\" int,\r\n  \"imageId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"gameEvents\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"key\" varchar UNIQUE,\r\n  \"time\" varchar,\r\n  \"gameTime\" varchar,\r\n  \"eventDescription\" varchar,\r\n  \"gameId\" int,\r\n  \"eventId\" int,\r\n  \"athleteId\" int,\r\n  \"userId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"gameAthletes\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"status\" int,\r\n  \"gameId\" int,\r\n  \"athleteId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"games\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"timeStart\" varchar,\r\n  \"timeEnd\" varchar,\r\n  \"homeColor\" varchar,\r\n  \"visitorColor\" varchar,\r\n  \"homeScore\" int,\r\n  \"visitorScore\" int,\r\n  \"homePenaltyScore\" int,\r\n  \"visitorPenaltyScore\" int,\r\n  \"status\" int,\r\n  \"type\" varchar,\r\n  \"location\" varchar,\r\n  \"homeId\" int,\r\n  \"visitorId\" int,\r\n  \"competitionId\" int,\r\n  \"imageId\" int,\r\n  \"userId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"gameUsers\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"userId\" int,\r\n  \"gameId\" int,\r\n  \"athleteId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"sports\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"name\" varchar,\r\n  \"imageId\" int,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nCREATE TABLE \"images\" (\r\n  \"id\" SERIAL PRIMARY KEY,\r\n  \"image\" varchar,\r\n  \"imageUrl\" varchar,\r\n  \"created\" varchar,\r\n  \"updated\" varchar\r\n);\r\n\r\nALTER TABLE \"users\" ADD FOREIGN KEY (\"imageId\") REFERENCES \"images\" (\"id\");\r\n\r\nALTER TABLE \"athletes\" ADD FOREIGN KEY (\"clubId\") REFERENCES \"clubs\" (\"id\");\r\n\r\nALTER TABLE \"athletes\" ADD FOREIGN KEY (\"imageId\") REFERENCES \"images\" (\"id\");\r\n\r\nALTER TABLE \"clubs\" ADD FOREIGN KEY (\"imageId\") REFERENCES \"images\" (\"id\");\r\n\r\nALTER TABLE \"competitions\" ADD FOREIGN KEY (\"sportId\") REFERENCES \"sports\" (\"id\");\r\n\r\nALTER TABLE \"competitions\" ADD FOREIGN KEY (\"imageId\") REFERENCES \"images\" (\"id\");\r\n\r\nALTER TABLE \"competitionBoards\" ADD FOREIGN KEY (\"clubId\") REFERENCES \"clubs\" (\"id\");\r\n\r\nALTER TABLE \"competitionBoards\" ADD FOREIGN KEY (\"competitionId\") REFERENCES \"competitions\" (\"id\");\r\n\r\nALTER TABLE \"events\" ADD FOREIGN KEY (\"sportId\") REFERENCES \"sports\" (\"id\");\r\n\r\nALTER TABLE \"events\" ADD FOREIGN KEY (\"imageId\") REFERENCES \"images\" (\"id\");\r\n\r\nALTER TABLE \"gameEvents\" ADD FOREIGN KEY (\"gameId\") REFERENCES \"games\" (\"id\");\r\n\r\nALTER TABLE \"gameEvents\" ADD FOREIGN KEY (\"eventId\") REFERENCES \"events\" (\"id\");\r\n\r\nALTER TABLE \"gameEvents\" ADD FOREIGN KEY (\"athleteId\") REFERENCES \"athletes\" (\"id\");\r\n\r\nALTER TABLE \"gameEvents\" ADD FOREIGN KEY (\"userId\") REFERENCES \"users\" (\"id\");\r\n\r\nALTER TABLE \"gameAthletes\" ADD FOREIGN KEY (\"gameId\") REFERENCES \"games\" (\"id\");\r\n\r\nALTER TABLE \"gameAthletes\" ADD FOREIGN KEY (\"athleteId\") REFERENCES \"athletes\" (\"id\");\r\n\r\nALTER TABLE \"games\" ADD FOREIGN KEY (\"homeId\") REFERENCES \"clubs\" (\"id\");\r\n\r\nALTER TABLE \"games\" ADD FOREIGN KEY (\"visitorId\") REFERENCES \"clubs\" (\"id\");\r\n\r\nALTER TABLE \"games\" ADD FOREIGN KEY (\"competitionId\") REFERENCES \"competitions\" (\"id\");\r\n\r\nALTER TABLE \"games\" ADD FOREIGN KEY (\"imageId\") REFERENCES \"images\" (\"id\");\r\n\r\nALTER TABLE \"games\" ADD FOREIGN KEY (\"userId\") REFERENCES \"users\" (\"id\");\r\n\r\nALTER TABLE \"gameUsers\" ADD FOREIGN KEY (\"userId\") REFERENCES \"users\" (\"id\");\r\n\r\nALTER TABLE \"gameUsers\" ADD FOREIGN KEY (\"gameId\") REFERENCES \"games\" (\"id\");\r\n\r\nALTER TABLE \"gameUsers\" ADD FOREIGN KEY (\"athleteId\") REFERENCES \"athletes\" (\"id\");\r\n\r\nALTER TABLE \"sports\" ADD FOREIGN KEY (\"imageId\") REFERENCES \"images\" (\"id\");\r\n\r\nCOMMENT ON COLUMN \"gameEvents\".\"key\" IS 'timestamp$userId';\r\n";

    }
}
