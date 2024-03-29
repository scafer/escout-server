INSERT INTO "users" ("username", "password", "email", "accessLevel", "notifications", "status", "imageId", "dataMap",
                     "created", "updated")
VALUES ('admin', '$2a$11$DJmDolzaMdt4Di/hrUAT9./c/OSSkzNHf.uQwIWtWySMzIDZCN9aG', 'admin@local.local', 2, 0, 0, NULL,
        NULL, '2022/02/10 14:36:20', '2022/02/10 14:45:32');

INSERT INTO "competitions" ("key", "name", "edition", "sportId", "imageId", "created", "updated")
VALUES (NULL, 'Liga dos Animais', '1ª Edição', 1, NULL, '2020/09/22 13:22:56', '2020/09/22 13:22:56');
INSERT INTO "competitions" ("key", "name", "edition", "sportId", "imageId", "created", "updated")
VALUES (NULL, 'Liga dos Abacates', '4ª Edição', 1, NULL, '2020/09/22 13:23:27', '2020/09/22 13:23:27');
INSERT INTO "competitions" ("key", "name", "edition", "sportId", "imageId", "created", "updated")
VALUES (NULL, 'Liga dos Mares', '6ª Edição', 1, NULL, '2020/09/22 13:23:56', '2020/09/22 13:23:56');


INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'KoalaFC', 'Koala Futebol Clube', 'Portugal', 'Fevereiro 2004', 'Cinzento e Castanho', NULL, NULL, NULL,
        NULL, NULL, '2020/09/22 12:58:21', '2020/09/22 12:59:42');
INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'CATartarugas', 'Clube Atlético das Tartarugas', 'Portugal', 'Abril 2008', 'Verde e Castanho', NULL, NULL,
        NULL, NULL, NULL, '2020/09/22 13:02:02', '2020/09/22 13:02:02');
INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'LBF', 'Lobo Ibérico de Futebol', 'Portugal', 'Junho 1999', 'Cinzento e Branco', NULL, NULL, NULL, NULL,
        NULL, '2020/09/22 13:04:12', '2020/09/22 13:04:12');
INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'CDPandas', 'Clube Desportivo dos Pandas', 'Portugal', 'Agosto 1995', 'Branco e Preto', NULL, NULL, NULL,
        NULL, NULL, '2020/09/22 13:05:38', '2020/09/22 13:05:38');
INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'FCBorboletas', 'Futebol Clube Borboletas', 'Portugal', 'Outubro 2010', 'Laranja e Preto', NULL, NULL,
        NULL, NULL, NULL, '2020/09/22 13:07:55', '2020/09/22 13:07:55');
INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'ÁguiaIP', 'Águia Imperial de Portugal', 'Portugal', 'Janeiro 1875', 'Castanho e Branco', NULL, NULL,
        NULL, NULL, NULL, '2020/09/22 13:09:49', '2020/09/22 13:09:49');
INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'UDTigres', 'União Desportiva dos Tigres', 'Portugal', 'Março 2002', 'Laranja e Preto', NULL, NULL, NULL,
        NULL, NULL, '2020/09/22 13:12:27', '2020/09/22 13:12:27');
INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'LSC', 'Linces Sport Clube', 'Portugal', 'Agosto 1994', 'Cinzento e Preto', NULL, NULL, NULL, NULL, NULL,
        '2020/09/22 13:15:26', '2020/09/22 13:16:17');
INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'CFAbelhas', 'Clube de Futebol das Abelhas', 'Portugal', 'Setembro 1900', 'Amarelo e Preto', NULL, NULL,
        NULL, NULL, NULL, '2020/09/22 13:18:42', '2020/09/22 13:19:03');
INSERT INTO "clubs" ("key", "name", "fullname", "country", "founded", "colors", "members", "stadium", "address",
                     "homepage", "imageId", "created", "updated")
VALUES (NULL, 'AAGuaxinis', 'Associação Académica Guaxinis', 'Portugal', 'Abril 2006', 'Castanho e Preto', NULL, NULL,
        NULL, NULL, NULL, '2020/09/22 13:21:59', '2020/09/22 13:21:59');


INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'RBraz', 'Rúben Braz', NULL, NULL, NULL, 170, 70, 'Defesa', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:41:11', '2020/09/22 13:41:11', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'JMarques', 'Jaime Marques', NULL, NULL, NULL, 170, 70, 'Defesa', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:42:27', '2020/09/22 13:42:27', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'SCosta', 'Simão Costa', NULL, NULL, NULL, 170, 70, 'Guarda-Redes', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:40:13', '2020/09/22 13:42:44', 1);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'ASilva', 'André Silva', NULL, NULL, NULL, 170, 70, 'Defesa', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:43:30', '2020/09/22 13:43:30', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'VPantaleão', 'Vasco Pantaleão', NULL, NULL, NULL, 170, 70, 'Defesa', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:44:24', '2020/09/22 13:44:24', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'NVerissímo', 'Nuno Verissímo', NULL, NULL, NULL, 170, 70, 'Médio', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:45:03', '2020/09/22 13:45:03', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'DSantos', 'Daniel Santos', NULL, NULL, NULL, 170, 70, 'Médio', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:47:01', '2020/09/22 13:47:01', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'DSeabra', 'Diogo Valente', NULL, NULL, NULL, 170, 70, 'Avançado', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:49:48', '2020/09/22 13:49:48', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'MTeixeira', 'Mário Teixeira', NULL, NULL, NULL, 156, 55, 'Avançado', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:49:07', '2020/09/22 13:50:05', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'BCarrazedo', 'Bruno Carrazedo', NULL, NULL, NULL, 170, 70, 'Médio', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:46:02', '2020/09/22 13:50:30', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'BCastro', 'Bruno Castro', NULL, NULL, NULL, 170, 70, 'Avançado', NULL, NULL, NULL, 1, NULL,
        '2020/09/22 13:51:11', '2020/09/22 13:51:11', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'SFerreira', 'Sandro Ferreira', NULL, NULL, NULL, 90, 177, 'Avançado', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:36:54', '2020/09/22 13:37:19', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'RVargas', 'Rodrigo Vargas', NULL, NULL, NULL, 170, 70, 'Defesa', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:38:34', '2020/09/22 13:38:34', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'JMartins', 'João Martins', NULL, NULL, NULL, 170, 70, 'Guarda-Redes', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:29:46', '2020/09/22 13:29:46', 1);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'MDuarte', 'Marco Duarte', NULL, NULL, NULL, 170, 70, 'Defesa', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:30:24', '2020/09/22 13:30:24', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'DFerreira', 'Dário Ferreira', NULL, NULL, NULL, 170, 70, 'Médio', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:31:09', '2020/09/22 13:31:09', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'DValente', 'Diogo Valente', NULL, NULL, NULL, 170, 70, 'Defesa', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:32:08', '2020/09/22 13:32:08', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'ARouco', 'André Rouco', NULL, NULL, NULL, 170, 70, 'Defesa', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:33:24', '2020/09/22 13:33:24', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'SPires', 'Sandro Pires', NULL, NULL, NULL, 170, 70, 'Avançado', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:33:55', '2020/09/22 13:33:55', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'RNunes', 'Rodrigo Nunes', NULL, NULL, NULL, 170, 70, 'Médio', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:34:40', '2020/09/22 13:34:40', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'FAnselmo', 'Fábio Anselmo', NULL, NULL, NULL, 170, 70, 'Avançado', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:35:26', '2020/09/22 13:35:26', 0);
INSERT INTO "athletes" ("key", "name", "fullname", "birthDate", "birthPlace", "citizenship", "height", "weight",
                        "position", "agent", "currentInternational", "status", "clubId", "imageId", "created",
                        "updated", "positionKey")
VALUES (NULL, 'MMatos', 'Miguel Matos', NULL, NULL, NULL, 170, 70, 'Médio', NULL, NULL, NULL, 2, NULL,
        '2020/09/22 13:36:06', '2020/09/22 13:36:06', 0);


INSERT INTO "games" ("timeStart", "timeEnd", "homeScore", "visitorScore", "homePenaltyScore", "visitorPenaltyScore",
                     "status", "homeId", "visitorId", "competitionId", "imageId", "userId", "created", "updated",
                     "type", "location")
VALUES ('2020/06/30 17:00:00', '2020/06/30 19:30:00', 0, 0, 0, 0, 0, 1, 2, 1, NULL, 1, '2020/09/22 14:09:01',
        '2020/09/22 14:09:01', 'Amigável', 'Portugal');
INSERT INTO "games" ("timeStart", "timeEnd", "homeScore", "visitorScore", "homePenaltyScore", "visitorPenaltyScore",
                     "status", "homeId", "visitorId", "competitionId", "imageId", "userId", "created", "updated",
                     "type", "location")
VALUES ('2020/06/19 17:00:00', '2020/06/19 19:30:00', 0, 0, 0, 0, 0, 2, 3, 1, NULL, 1, '2020/09/22 14:11:25',
        '2020/09/22 14:11:25', 'Amigável', 'Portugal');
INSERT INTO "games" ("timeStart", "timeEnd", "homeScore", "visitorScore", "homePenaltyScore", "visitorPenaltyScore",
                     "status", "homeId", "visitorId", "competitionId", "imageId", "userId", "created", "updated",
                     "type", "location")
VALUES ('2020/01/18 17:00:00', '2020/01/18 17:00:00', 0, 0, 0, 0, 0, 2, 4, NULL, NULL, 1, '2020/09/22 14:14:40',
        '2020/09/22 14:14:40', 'Amigável', 'Portugal');
INSERT INTO "games" ("timeStart", "timeEnd", "homeScore", "visitorScore", "homePenaltyScore", "visitorPenaltyScore",
                     "status", "homeId", "visitorId", "competitionId", "imageId", "userId", "created", "updated",
                     "type", "location")
VALUES ('2020/04/22 17:00:00', '2020/04/22 19:30:00', 0, 0, 0, 0, 0, 3, 5, NULL, NULL, 1, '2020/09/22 14:20:24',
        '2020/09/22 14:20:24', 'Amigávels', 'Portugal');
INSERT INTO "games" ("timeStart", "timeEnd", "homeScore", "visitorScore", "homePenaltyScore", "visitorPenaltyScore",
                     "status", "homeId", "visitorId", "competitionId", "imageId", "userId", "created", "updated",
                     "type", "location")
VALUES ('2020/09/12 17:00:00', '2020/09/12 19:30:00', 0, 0, 0, 0, 0, 3, 6, NULL, NULL, 1, '2020/09/22 14:21:17',
        '2020/09/22 14:21:17', 'Amigável', 'Portugal');
