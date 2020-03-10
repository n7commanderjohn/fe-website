-- USE dev_fewebsite;
USE fewebsite;

DELETE
	FROM userlikes
	WHERE LikerId IN (3, 5, 6, 7, 8, 9, 13) OR LikeeId IN (3, 5, 6, 7, 8, 9, 13);

DELETE
	FROM usermessages
	WHERE senderid IN (3, 5, 6, 7, 8, 9, 13) OR RecipientId IN (3, 5, 6, 7, 8, 9, 13);
	
DELETE
	FROM users
	WHERE id IN (3, 5, 6, 7, 8, 9, 13);
	
SELECT *
	FROM userlikes
	WHERE likerid IN (3, 5, 6, 7, 8, 9, 13) OR likeeid IN (3, 5, 6, 7, 8, 9, 13);
	
SELECT *
	FROM usermessages
	WHERE senderid IN (3, 5, 6, 7, 8, 9, 13) OR recipientid IN (3, 5, 6, 7, 8, 9, 13);
	
SELECT *
FROM users;