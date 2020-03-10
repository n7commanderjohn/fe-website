-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.19 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             10.1.0.5464
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping data for table fewebsite.gamegenres: ~4 rows (approximately)
DELETE FROM `gamegenres`;
/*!40000 ALTER TABLE `gamegenres` DISABLE KEYS */;
INSERT INTO `gamegenres` (`Id`, `Description`, `Name`) VALUES
	(1, 'Action', 'AC'),
	(2, 'Adventure', 'AD'),
	(3, 'Role Playing Game', 'RPG'),
	(4, 'Strategy', 'S');
/*!40000 ALTER TABLE `gamegenres` ENABLE KEYS */;

-- Dumping data for table fewebsite.games: ~16 rows (approximately)
DELETE FROM `games`;
/*!40000 ALTER TABLE `games` DISABLE KEYS */;
INSERT INTO `games` (`Id`, `Description`, `Name`) VALUES
	(1, 'Fire Emblem 1', 'FE1'),
	(2, 'Fire Emblem 2', 'FE2'),
	(3, 'Fire Emblem 3', 'FE3'),
	(4, 'Fire Emblem 4', 'FE4'),
	(5, 'Fire Emblem 5', 'FE5'),
	(6, 'Fire Emblem 6', 'FE6'),
	(7, 'Fire Emblem 7', 'FE7'),
	(8, 'Fire Emblem 8', 'FE8'),
	(9, 'Fire Emblem 9', 'FE9'),
	(10, 'Fire Emblem 10', 'FE10'),
	(11, 'Fire Emblem 11', 'FE11'),
	(12, 'Fire Emblem 12', 'FE12'),
	(13, 'Fire Emblem 13', 'FE13'),
	(14, 'Fire Emblem 14', 'FE14'),
	(15, 'Fire Emblem 15', 'FE15'),
	(16, 'Fire Emblem 16', 'FE16');
/*!40000 ALTER TABLE `games` ENABLE KEYS */;

-- Dumping data for table fewebsite.genders: ~3 rows (approximately)
DELETE FROM `genders`;
/*!40000 ALTER TABLE `genders` DISABLE KEYS */;
INSERT INTO `genders` (`Id`, `Description`) VALUES
	('F', 'Female'),
	('M', 'Male'),
	('NA', 'Prefer not to say');
/*!40000 ALTER TABLE `genders` ENABLE KEYS */;

-- Dumping data for table fewebsite.photos: ~104 rows (approximately)
DELETE FROM `photos`;
/*!40000 ALTER TABLE `photos` DISABLE KEYS */;
INSERT INTO `photos` (`Id`, `Description`, `Name`, `UserId`, `Url`, `DateAdded`, `IsMain`, `PublicId`) VALUES
	(1, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1579984470/ou6vccumvisalxhskcq4.jpg', '2020-01-25 15:34:29.719594', 0, 'ou6vccumvisalxhskcq4'),
	(2, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1579984484/zsioyswikx5wusbukeot.jpg', '2020-01-25 15:34:43.577329', 0, 'zsioyswikx5wusbukeot'),
	(3, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580157128/shtmbjgki8sktvtleqys.png', '2020-01-27 15:32:07.617793', 1, 'shtmbjgki8sktvtleqys'),
	(4, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019680/bfyzqmnxscuuv1wezd29.jpg', '2020-01-26 01:21:20.060837', 0, 'bfyzqmnxscuuv1wezd29'),
	(5, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019681/iitjrz6newhbiou1kfne.jpg', '2020-01-26 01:21:20.473020', 0, 'iitjrz6newhbiou1kfne'),
	(6, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019682/whxeq1ezrmyte4q4xa1o.jpg', '2020-01-26 01:21:21.155102', 0, 'whxeq1ezrmyte4q4xa1o'),
	(7, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019683/wnw7kupwgnnbhuaq8sqx.jpg', '2020-01-26 01:21:22.338795', 0, 'wnw7kupwgnnbhuaq8sqx'),
	(8, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019683/x9z1f37u9g2lxmgzilvc.jpg', '2020-01-26 01:21:23.319526', 0, 'x9z1f37u9g2lxmgzilvc'),
	(9, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1579985288/q3sty6k55p4wn1c1mexv.jpg', '2020-01-25 15:48:07.216931', 0, 'q3sty6k55p4wn1c1mexv'),
	(10, NULL, NULL, 2, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580121957/dnjojcbv6mhrdxplrjc7.jpg', '2020-01-27 05:45:56.489951', 1, 'dnjojcbv6mhrdxplrjc7'),
	(11, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580020112/jkwoxjjmompyxcwbrmrt.jpg', '2020-01-26 01:28:28.238534', 0, 'jkwoxjjmompyxcwbrmrt'),
	(12, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156503/begvuppry7ov7zi0rjem.jpg', '2020-01-27 15:21:42.602076', 0, 'begvuppry7ov7zi0rjem'),
	(13, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580020115/tr9g2j9ymu4lkarfzbsb.jpg', '2020-01-26 01:28:34.213957', 0, 'tr9g2j9ymu4lkarfzbsb'),
	(14, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1579984470/ou6vccumvisalxhskcq4.jpg', '2020-01-25 15:34:29.719594', 0, 'ou6vccumvisalxhskcq4'),
	(15, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1579984484/zsioyswikx5wusbukeot.jpg', '2020-01-25 15:34:43.577329', 0, 'zsioyswikx5wusbukeot'),
	(16, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1579985288/q3sty6k55p4wn1c1mexv.jpg', '2020-01-25 15:48:07.216931', 0, 'q3sty6k55p4wn1c1mexv'),
	(17, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156503/begvuppry7ov7zi0rjem.jpg', '2020-01-27 15:21:42.602076', 0, 'begvuppry7ov7zi0rjem'),
	(18, NULL, NULL, 2, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580121955/gcoss7gsolq3gh5utanq.jpg', '2020-01-27 05:45:54.723986', 0, 'gcoss7gsolq3gh5utanq'),
	(19, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156502/vbmyyyesf9pq3sycs9qp.jpg', '2020-01-27 15:21:41.722794', 0, 'vbmyyyesf9pq3sycs9qp'),
	(20, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580009742/ipp83tpqonadxlfwbocx.png', '2020-01-25 22:35:28.626804', 0, 'ipp83tpqonadxlfwbocx'),
	(21, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580010128/jlvpp3dob1lf6sshw7mg.jpg', '2020-01-25 22:42:08.077722', 0, 'jlvpp3dob1lf6sshw7mg'),
	(22, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580010129/t9xojhgayqmuiok9agfa.jpg', '2020-01-25 22:42:08.932007', 0, 't9xojhgayqmuiok9agfa'),
	(23, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019676/qwcc3zlatjlp9mjlswmb.jpg', '2020-01-26 01:21:14.276889', 0, 'qwcc3zlatjlp9mjlswmb'),
	(24, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019674/gahzmwvfweb3dv00c1ee.jpg', '2020-01-26 01:21:13.743507', 0, 'gahzmwvfweb3dv00c1ee'),
	(25, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019673/fyyohb8pbvdqfgrx6j40.jpg', '2020-01-26 01:21:12.748798', 0, 'fyyohb8pbvdqfgrx6j40'),
	(26, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580016035/u4y8octagi7irgg3z4qx.png', '2020-01-26 00:20:35.180125', 0, 'u4y8octagi7irgg3z4qx'),
	(27, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580016034/bsi94yfaoeedyhhnj2ps.png', '2020-01-26 00:20:30.112630', 0, 'bsi94yfaoeedyhhnj2ps'),
	(28, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580016029/tf65ewmyudrqi4bofphr.jpg', '2020-01-26 00:20:29.188085', 0, 'tf65ewmyudrqi4bofphr'),
	(29, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580010128/jlvpp3dob1lf6sshw7mg.jpg', '2020-01-25 22:42:08.077722', 0, 'jlvpp3dob1lf6sshw7mg'),
	(30, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580014840/dqf9uvxnamoniijijgbs.jpg', '2020-01-26 00:00:40.158734', 0, 'dqf9uvxnamoniijijgbs'),
	(31, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015867/j8pa8qzmzhu5aoyl1ode.jpg', '2020-01-26 00:17:47.152760', 0, 'j8pa8qzmzhu5aoyl1ode'),
	(32, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580014861/ibvnxaxhhzfre9i7l3gg.jpg', '2020-01-26 00:01:01.500380', 0, 'ibvnxaxhhzfre9i7l3gg'),
	(33, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015804/pykoducqttwbswdtqfod.jpg', '2020-01-26 00:16:43.665966', 0, 'pykoducqttwbswdtqfod'),
	(34, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015803/w8shh4eemt1ah3g4ulqm.jpg', '2020-01-26 00:16:43.204281', 0, 'w8shh4eemt1ah3g4ulqm'),
	(35, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015800/nfeyeouzx9nwmyb4bq2q.jpg', '2020-01-26 00:16:40.054605', 0, 'nfeyeouzx9nwmyb4bq2q'),
	(36, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015800/nfeyeouzx9nwmyb4bq2q.jpg', '2020-01-26 00:16:40.054605', 0, 'nfeyeouzx9nwmyb4bq2q'),
	(37, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580014840/dqf9uvxnamoniijijgbs.jpg', '2020-01-26 00:00:40.158734', 0, 'dqf9uvxnamoniijijgbs'),
	(38, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015803/w8shh4eemt1ah3g4ulqm.jpg', '2020-01-26 00:16:43.204281', 0, 'w8shh4eemt1ah3g4ulqm'),
	(39, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015804/pykoducqttwbswdtqfod.jpg', '2020-01-26 00:16:43.665966', 0, 'pykoducqttwbswdtqfod'),
	(40, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015805/vcjrb9z86gzzeuekybgj.jpg', '2020-01-26 00:16:44.269715', 0, 'vcjrb9z86gzzeuekybgj'),
	(41, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580020121/dzle99ogjti2l3r2u5gp.png', '2020-01-26 01:28:40.257628', 0, 'dzle99ogjti2l3r2u5gp'),
	(42, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015805/vcjrb9z86gzzeuekybgj.jpg', '2020-01-26 00:16:44.269715', 0, 'vcjrb9z86gzzeuekybgj'),
	(43, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015867/j8pa8qzmzhu5aoyl1ode.jpg', '2020-01-26 00:17:47.152760', 0, 'j8pa8qzmzhu5aoyl1ode'),
	(44, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015803/w8shh4eemt1ah3g4ulqm.jpg', '2020-01-26 00:16:43.204281', 0, 'w8shh4eemt1ah3g4ulqm'),
	(45, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015869/ljpgvpyi2yasmcvedhex.png', '2020-01-26 00:17:49.053836', 0, 'ljpgvpyi2yasmcvedhex'),
	(46, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580015871/pc8hp6lyxvhqzlcgtpky.jpg', '2020-01-26 00:17:50.585649', 0, 'pc8hp6lyxvhqzlcgtpky'),
	(47, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580014840/dqf9uvxnamoniijijgbs.jpg', '2020-01-26 00:00:40.158734', 0, 'dqf9uvxnamoniijijgbs'),
	(48, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580016029/tf65ewmyudrqi4bofphr.jpg', '2020-01-26 00:20:29.188085', 0, 'tf65ewmyudrqi4bofphr'),
	(49, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580016034/bsi94yfaoeedyhhnj2ps.png', '2020-01-26 00:20:30.112630', 0, 'bsi94yfaoeedyhhnj2ps'),
	(50, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580016035/u4y8octagi7irgg3z4qx.png', '2020-01-26 00:20:35.180125', 0, 'u4y8octagi7irgg3z4qx'),
	(51, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019673/fyyohb8pbvdqfgrx6j40.jpg', '2020-01-26 01:21:12.748798', 0, 'fyyohb8pbvdqfgrx6j40'),
	(52, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019674/gahzmwvfweb3dv00c1ee.jpg', '2020-01-26 01:21:13.743507', 0, 'gahzmwvfweb3dv00c1ee'),
	(53, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019676/qwcc3zlatjlp9mjlswmb.jpg', '2020-01-26 01:21:14.276889', 0, 'qwcc3zlatjlp9mjlswmb'),
	(54, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019676/wor8wfeilem1gjzddhgg.png', '2020-01-26 01:21:16.227932', 0, 'wor8wfeilem1gjzddhgg'),
	(55, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019679/orlhiiljcqjofflsn12t.jpg', '2020-01-26 01:21:18.631602', 0, 'orlhiiljcqjofflsn12t'),
	(56, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019679/gjw0vf2zim8tciokafkq.gif', '2020-01-26 01:21:19.124884', 0, 'gjw0vf2zim8tciokafkq'),
	(57, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019680/bfyzqmnxscuuv1wezd29.jpg', '2020-01-26 01:21:20.060837', 0, 'bfyzqmnxscuuv1wezd29'),
	(58, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019681/iitjrz6newhbiou1kfne.jpg', '2020-01-26 01:21:20.473020', 0, 'iitjrz6newhbiou1kfne'),
	(59, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019682/whxeq1ezrmyte4q4xa1o.jpg', '2020-01-26 01:21:21.155102', 0, 'whxeq1ezrmyte4q4xa1o'),
	(60, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019683/wnw7kupwgnnbhuaq8sqx.jpg', '2020-01-26 01:21:22.338795', 0, 'wnw7kupwgnnbhuaq8sqx'),
	(61, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019683/x9z1f37u9g2lxmgzilvc.jpg', '2020-01-26 01:21:23.319526', 0, 'x9z1f37u9g2lxmgzilvc'),
	(62, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580020112/jkwoxjjmompyxcwbrmrt.jpg', '2020-01-26 01:28:28.238534', 0, 'jkwoxjjmompyxcwbrmrt'),
	(63, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580020115/tr9g2j9ymu4lkarfzbsb.jpg', '2020-01-26 01:28:34.213957', 0, 'tr9g2j9ymu4lkarfzbsb'),
	(64, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580020115/tr9g2j9ymu4lkarfzbsb.jpg', '2020-01-26 01:28:34.213957', 0, 'tr9g2j9ymu4lkarfzbsb'),
	(65, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580020117/clydpauxudbzq9pwirsa.png', '2020-01-26 01:28:35.688590', 0, 'clydpauxudbzq9pwirsa'),
	(66, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580020119/di1kzwytgtrg8a2nsjjy.png', '2020-01-26 01:28:38.129755', 0, 'di1kzwytgtrg8a2nsjjy'),
	(67, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580020121/dzle99ogjti2l3r2u5gp.png', '2020-01-26 01:28:40.257628', 0, 'dzle99ogjti2l3r2u5gp'),
	(68, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019682/whxeq1ezrmyte4q4xa1o.jpg', '2020-01-26 01:21:21.155102', 0, 'whxeq1ezrmyte4q4xa1o'),
	(69, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019681/iitjrz6newhbiou1kfne.jpg', '2020-01-26 01:21:20.473020', 0, 'iitjrz6newhbiou1kfne'),
	(70, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019680/bfyzqmnxscuuv1wezd29.jpg', '2020-01-26 01:21:20.060837', 0, 'bfyzqmnxscuuv1wezd29'),
	(71, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019679/gjw0vf2zim8tciokafkq.gif', '2020-01-26 01:21:19.124884', 0, 'gjw0vf2zim8tciokafkq'),
	(72, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019679/orlhiiljcqjofflsn12t.jpg', '2020-01-26 01:21:18.631602', 0, 'orlhiiljcqjofflsn12t'),
	(73, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019676/wor8wfeilem1gjzddhgg.png', '2020-01-26 01:21:16.227932', 0, 'wor8wfeilem1gjzddhgg'),
	(74, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019676/qwcc3zlatjlp9mjlswmb.jpg', '2020-01-26 01:21:14.276889', 0, 'qwcc3zlatjlp9mjlswmb'),
	(75, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019674/gahzmwvfweb3dv00c1ee.jpg', '2020-01-26 01:21:13.743507', 0, 'gahzmwvfweb3dv00c1ee'),
	(76, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019673/fyyohb8pbvdqfgrx6j40.jpg', '2020-01-26 01:21:12.748798', 0, 'fyyohb8pbvdqfgrx6j40'),
	(77, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580016035/u4y8octagi7irgg3z4qx.png', '2020-01-26 00:20:35.180125', 0, 'u4y8octagi7irgg3z4qx'),
	(78, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580016034/bsi94yfaoeedyhhnj2ps.png', '2020-01-26 00:20:30.112630', 0, 'bsi94yfaoeedyhhnj2ps'),
	(79, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019679/orlhiiljcqjofflsn12t.jpg', '2020-01-26 01:21:18.631602', 0, 'orlhiiljcqjofflsn12t'),
	(80, NULL, NULL, 3, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580019679/gjw0vf2zim8tciokafkq.gif', '2020-01-26 01:21:19.124884', 0, 'gjw0vf2zim8tciokafkq'),
	(102, NULL, NULL, 11, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580121194/tokcfgkhxcqxb3shbmuu.jpg', '2020-01-27 05:33:13.824961', 1, 'tokcfgkhxcqxb3shbmuu'),
	(103, NULL, NULL, 11, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580121194/ir5ra1wlg9chbvilhnsc.jpg', '2020-01-27 05:33:14.374160', 0, 'ir5ra1wlg9chbvilhnsc'),
	(104, NULL, NULL, 11, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580121196/bapv90rlhzz4stsmu9zx.jpg', '2020-01-27 05:33:15.180982', 0, 'bapv90rlhzz4stsmu9zx'),
	(105, NULL, NULL, 11, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580121197/engjmhz6k6v0vrooo20h.jpg', '2020-01-27 05:33:16.302261', 0, 'engjmhz6k6v0vrooo20h'),
	(106, NULL, NULL, 11, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580121197/v76qr3zvfotu1aseffyz.jpg', '2020-01-27 05:33:17.175219', 0, 'v76qr3zvfotu1aseffyz'),
	(115, NULL, NULL, 2, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580121955/gcoss7gsolq3gh5utanq.jpg', '2020-01-27 05:45:54.723986', 0, 'gcoss7gsolq3gh5utanq'),
	(117, NULL, NULL, 2, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580121957/dnjojcbv6mhrdxplrjc7.jpg', '2020-01-27 05:45:56.489951', 1, 'dnjojcbv6mhrdxplrjc7'),
	(119, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156500/pnkatvphe5v8lvpbwnlw.jpg', '2020-01-27 15:21:38.780745', 0, 'pnkatvphe5v8lvpbwnlw'),
	(120, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156501/dy6pqwv7m0zspgbwp5ez.jpg', '2020-01-27 15:21:40.136607', 0, 'dy6pqwv7m0zspgbwp5ez'),
	(121, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156502/vbmyyyesf9pq3sycs9qp.jpg', '2020-01-27 15:21:41.722794', 0, 'vbmyyyesf9pq3sycs9qp'),
	(122, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156503/begvuppry7ov7zi0rjem.jpg', '2020-01-27 15:21:42.602076', 0, 'begvuppry7ov7zi0rjem'),
	(123, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156798/tzcmuzdonhqmrey9fknm.jpg', '2020-01-27 15:26:37.362667', 0, 'tzcmuzdonhqmrey9fknm'),
	(124, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156817/nmp0ernao2zqnqj1crcq.png', '2020-01-27 15:26:55.513200', 0, 'nmp0ernao2zqnqj1crcq'),
	(125, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156821/lgvzxs1mrlddscpvanmu.jpg', '2020-01-27 15:27:01.090029', 0, 'lgvzxs1mrlddscpvanmu'),
	(126, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156822/aiisqbom1huq2vlbvcgn.jpg', '2020-01-27 15:27:01.846491', 0, 'aiisqbom1huq2vlbvcgn'),
	(127, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156824/hynmltw15m0ea4mfrdub.jpg', '2020-01-27 15:27:02.840287', 0, 'hynmltw15m0ea4mfrdub'),
	(128, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156825/obesyqgbc05gaznqhavc.png', '2020-01-27 15:27:04.830086', 0, 'obesyqgbc05gaznqhavc'),
	(129, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156827/mbz63upiuqnjlpbqsbjq.jpg', '2020-01-27 15:27:06.538279', 0, 'mbz63upiuqnjlpbqsbjq'),
	(130, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156828/wgowgijmsoz5eezrfoa4.jpg', '2020-01-27 15:27:07.505957', 0, 'wgowgijmsoz5eezrfoa4'),
	(131, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156828/vlumfdobsseijmtzvv5v.jpg', '2020-01-27 15:27:08.271847', 0, 'vlumfdobsseijmtzvv5v'),
	(132, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580156867/uqvfkpxelaqhph0ml1ko.png', '2020-01-27 15:27:47.167818', 0, 'uqvfkpxelaqhph0ml1ko'),
	(134, NULL, NULL, 4, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580157007/hdvgjckz9cqauy30blgr.jpg', '2020-01-27 15:30:06.694974', 1, 'hdvgjckz9cqauy30blgr'),
	(135, NULL, NULL, 1, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580157128/shtmbjgki8sktvtleqys.png', '2020-01-27 15:32:07.617793', 1, 'shtmbjgki8sktvtleqys'),
	(139, NULL, NULL, 12, 'http://res.cloudinary.com/n7commanderjohn-com/image/upload/v1580157503/iriju9pnc9vtup5ywx6v.jpg', '2020-01-27 15:38:23.175140', 1, 'iriju9pnc9vtup5ywx6v');
/*!40000 ALTER TABLE `photos` ENABLE KEYS */;

-- Dumping data for table fewebsite.usergamegenres: ~11 rows (approximately)
DELETE FROM `usergamegenres`;
/*!40000 ALTER TABLE `usergamegenres` DISABLE KEYS */;
INSERT INTO `usergamegenres` (`UserId`, `GameGenreId`) VALUES
	(2, 1),
	(1, 2),
	(2, 2),
	(3, 2),
	(4, 2),
	(1, 3),
	(2, 3),
	(3, 3),
	(4, 3),
	(2, 4),
	(4, 4);
/*!40000 ALTER TABLE `usergamegenres` ENABLE KEYS */;

-- Dumping data for table fewebsite.usergames: ~14 rows (approximately)
DELETE FROM `usergames`;
/*!40000 ALTER TABLE `usergames` DISABLE KEYS */;
INSERT INTO `usergames` (`UserId`, `GameId`) VALUES
	(1, 7),
	(2, 7),
	(3, 7),
	(1, 8),
	(2, 8),
	(3, 8),
	(4, 8),
	(4, 10),
	(4, 13),
	(4, 14),
	(1, 16),
	(2, 16),
	(3, 16),
	(4, 16);
/*!40000 ALTER TABLE `usergames` ENABLE KEYS */;

-- Dumping data for table fewebsite.userlikes: ~5 rows (approximately)
DELETE FROM `userlikes`;
/*!40000 ALTER TABLE `userlikes` DISABLE KEYS */;
INSERT INTO `userlikes` (`LikerId`, `LikeeId`) VALUES
	(2, 1),
	(1, 2),
	(1, 4),
	(1, 11),
	(1, 12);
/*!40000 ALTER TABLE `userlikes` ENABLE KEYS */;

-- Dumping data for table fewebsite.usermessages: ~8 rows (approximately)
DELETE FROM `usermessages`;
/*!40000 ALTER TABLE `usermessages` DISABLE KEYS */;
INSERT INTO `usermessages` (`Id`, `SenderId`, `RecipientId`, `Content`, `IsRead`, `DateRead`, `MessageSent`, `SenderDeleted`, `RecipientDeleted`) VALUES
	(1, 1, 3, 'hi clonee!!!', 1, '2020-03-10 02:15:22.758228', '2020-03-10 02:14:42.688440', 0, 0),
	(2, 1, 2, 'Hi broooo', 1, '2020-03-06 22:31:40.417217', '2020-03-03 19:29:40.930727', 0, 0),
	(3, 1, 2, 'are you smashin Eirika', 1, '2020-03-06 22:31:40.419321', '2020-03-03 19:34:58.849984', 0, 0),
	(4, 2, 1, 'sure am!!!', 1, '2020-03-06 22:33:08.591148', '2020-03-04 02:10:43.686646', 0, 0),
	(5, 1, 2, 'that\'s so hawtttt!!!', 1, '2020-03-06 22:31:40.395779', '2020-03-04 02:13:46.658583', 0, 0),
	(86, 1, 2, 'zzzzz', 1, '2020-03-06 22:52:42.887626', '2020-03-06 22:35:00.750906', 0, 0),
	(87, 1, 2, 'i\'m soooo tiredddddd', 1, '2020-03-06 22:52:43.044426', '2020-03-06 22:35:14.034363', 0, 0),
	(89, 2, 1, 'jjj', 1, '2020-03-09 23:44:33.963823', '2020-03-09 02:28:03.172119', 0, 0);
/*!40000 ALTER TABLE `usermessages` ENABLE KEYS */;

-- Dumping data for table fewebsite.users: ~6 rows (approximately)
DELETE FROM `users`;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`Id`, `Email`, `Username`, `PasswordHash`, `PasswordSalt`, `Birthday`, `AccountCreated`, `LastLogin`, `Gender`, `AboutMe`, `Alias`) VALUES
	(1, 'kimgears2@gmail.com', 'n7cmdrjohn', _binary 0xF24BFE87D9540C423FFCAB0C3486C42BB25C859B3DB980C126D18A120B60008A44628F8225A7A10B493AE6D6DC0C843D620A7A286B3712BEC587547C8DF43707, _binary 0xACAA5A97CA2B4E560B3D25070E36C4BA7B0336CC22C572D2533741375F94B9973C60079243835876C447B2CD816EE6621777BC89983F4E22485D928ACB7616E28A1E3BF4A08934C99948E27BD7A4D0394A51289BF14F9427AD604A6814500A80B7191EB589EDF06236F2C3F34B5BB5C8B57A58C5CEAF4EFA14EFC022CF47F540, '1992-09-05 20:00:00.000000', '2015-01-30 00:00:00.000000', '2020-03-10 02:35:36.807112', 'NA', 'i will git gud', 'N7 John'),
	(2, 'iloveeirika6969@gmail.com', 'iloveeirika6969', _binary 0x88AA267B8F6C6100C5743AB488CD3791E0BBC313EE825AE5DC3A8422166866016D944898B679FAD2155E8F28B7CEE436B2FE90E2B2A8F7D5AE7C487397AE2D1D, _binary 0x485ED4FE753FEEF6B44B9E43A99BAD49ABA23416683E41076DF5AC71B5063DF20C79F1A28913B8BBAB077025174C9351AA84A54269C060E8CF855CA0A07944AA8307BC930271EA59EAFF99214F02FE695D80387577F431EE7E0E6EF1FC47D531051BC489BC9675C8164B9C2313EB92687F3AE01B560BE33B1E4B4D6FEA716025, '1991-05-15 08:00:00.000000', '2015-05-15 00:00:00.000000', '2020-03-10 02:36:17.635150', 'M', 'eirika is my waifu\r\nhell yeah\r\npronouns\r\ngot 2000 orbs saved for Eirika', 'Ephriam'),
	(3, 'kimgears3@gmail.com', 'n7cmdrjohn3', _binary 0x8A947D8F30796FCBE4A553A224B6D2054DA9A9E730248C3E7F67BFB2732176A3C9CB6C08D84DDA47AADBFE1AA06FAF7E8563FC0FC7E9F3B3A1A0DBF8292C8EEC, _binary 0xEC214667A76789D1282EF96B9C7D9C3BB4CFA89DAC8544B2CEC398D708A816B2A82876797DF19EDCDDEF8A0B8B5D32878CD2A9BF83FB4D7AA9E29BEC0F2F7459DFC9BDD0DD41DE01B4E57DC9C58762CCEC40669F4C559490F0F6827250C6284B9FB770DD006C355BA9CE8A24DD70E5579B3FEACF12619849A446803BDB610985, '1992-09-05 00:00:00.000000', '2015-01-30 00:00:00.000000', '2020-03-10 02:36:58.611402', 'NA', 'I am best pro\nand i will git gud', 'N7 John'),
	(4, 'stonelover4141@gmail.com', 'stonelover4141', _binary 0x550F2B2D5413B4744B817A73AF2748D2B6C38C5F82EEB7708622EC4FE26DE512AB98F529B9DADC0BC30B3D98502EAC2815CF9AC7FD951BB4B28C428119E1FE15, _binary 0x32388E5678AE0DFC42B07728B4B2B91DAA9A476F7D628B1BC1957AF37920BAF77DB1ABCFE15310B8B19728E7328F2961E7CA78B6EBA37E78BE610B5DA22AA6FADB01D4A9682715D6C7F0FC4E7814D217D6911DC996A8EB6958C3BB99F02B1A1617DEAB3BFA7F5EDBD3C9C302DEA60466E844324631AFBAE886E29501E7E96397, '1997-10-02 09:00:00.000000', '2018-05-06 00:00:00.000000', '2020-03-10 02:37:13.106917', 'F', 'HAHA I LOVE STONES', 'Furzz'),
	(11, 'blackjack@gmail.com', 'blackjack', _binary 0xB2F9D5CAF7A7FCEF3D652E5EF9F5CEAD19BD0117010646D872F459C88DC018002DB034A6C7F86B3D0726BE55C41F1B87FAF086A50374006040FFF9BADAFA2200, _binary 0xA97EB849D4320E07D00400D1DB6B8565A61FC1FFA535EEF663165C67C8910BFB0B3EF93DF8CFF6303013A986D0A7A208CBEA4F9802C283F516CC887AA20EC110E748EDEB497098B3A0EE2552AD1C752C32F2796BACABCCF6591EAB9BBC694CA2F08499038AFE686CC6282A73BFC42DE9A14C58C7DC78383673BE1F99D5C87392, '0001-01-01 00:00:00.000000', '2020-01-27 01:48:48.023685', '2020-03-10 02:38:05.621897', 'F', 'epic story of blackjack', 'Black of Jack'),
	(12, 'cmdrshepard@gmail.com', 'cmdrshepard', _binary 0x5801D7C49F9C298F7930EF82E5B3F48CF6636DDE338799348E1371F3764C7A15BFCD30E1A4D09EBAB450BDF47D0A8E568C9358F74FCE51B57A5B7A8E120A269F, _binary 0x99B5FB4C79ECE90C0A2EF4C9BEF46663160161D447E6D5DDC60D0E8D462AFB44C86B1F4B6FA45F2AE58D4CE7B77348ED075A0E4D507ACDE1AA9D1D81575141BB3A9C692685F7C9422409C9F87832BDBC755E73C6DD5D3A3309EC42EDCEB83F64638EB9601E3B7E421E8F26FB02A4DFCA4A36FF605F3B7641B58DCAEC6632DBA7, '0001-01-01 00:00:00.000000', '2020-01-27 15:25:47.304905', '2020-03-10 02:38:11.369726', 'M', 'I\'m Commander Shepard, and this is my favorite website on the Citadel.', 'Commander Shepard');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

-- Dumping data for table fewebsite.__efmigrationshistory: ~2 rows (approximately)
DELETE FROM `__efmigrationshistory`;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20200309194714_MySqlInit', '3.1.0'),
	('20200309202525_PopulateLookupTables', '3.1.0');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
