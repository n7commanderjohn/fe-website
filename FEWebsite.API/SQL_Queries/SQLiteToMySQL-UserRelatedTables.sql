INSERT INTO UserMessages (Id,SenderId,RecipientId,Content,IsRead,DateRead,MessageSent,SenderDeleted,RecipientDeleted) VALUES (2,1,2,'Hi broooo',1,'2020-03-06 22:31:40.417217','2020-03-03 19:29:40.9307269',0,0),
 (3,1,2,'are you smashin Eirika',1,'2020-03-06 22:31:40.4193212','2020-03-03 19:34:58.8499843',0,0),
 (4,2,1,'sure am!!!',1,'2020-03-06 22:33:08.5911476','2020-03-04 02:10:43.6866462',0,0),
 (5,1,2,'that''s so hawtttt!!!',1,'2020-03-06 22:31:40.3957786','2020-03-04 02:13:46.6585833',0,0),
 (86,1,2,'zzzzz',1,'2020-03-06 22:52:42.8876256','2020-03-06 22:35:00.7509064',0,0),
 (87,1,2,'i''m soooo tiredddddd',1,'2020-03-06 22:52:43.0444264','2020-03-06 22:35:14.0343628',0,0),
 (88,1,13,'ugh',0,NULL,'2020-03-07 01:08:01.5500343',0,0),
 (89,2,1,'jjj',0,NULL,'2020-03-09 02:28:03.1721194',0,0);
INSERT INTO UserLikes (LikerId,LikeeId) VALUES (1,4),
 (1,12),
 (1,5),
 (1,3),
 (1,11),
 (1,2),
 (2,1);
INSERT INTO UserGames (UserId,GameId) VALUES (9,3),
 (7,3),
 (7,9),
 (8,9),
 (9,11),
 (8,11),
 (8,3),
 (7,11),
 (9,9),
 (5,13),
 (6,9),
 (3,3),
 (6,11),
 (3,10),
 (4,10),
 (4,13),
 (5,3),
 (5,10),
 (6,3),
 (3,13),
 (1,7),
 (1,8),
 (1,16),
 (2,8),
 (4,8),
 (4,14),
 (4,16);
INSERT INTO UserGameGenres (UserId,GameGenreId) VALUES (6,1),
 (9,1),
 (5,4),
 (5,1),
 (9,3),
 (7,1),
 (7,3),
 (4,4),
 (3,4),
 (3,1),
 (8,1),
 (8,3),
 (6,3),
 (2,4),
 (2,1),
 (1,3),
 (4,2),
 (4,3);