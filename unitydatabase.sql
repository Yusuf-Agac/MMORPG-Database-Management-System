-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Apr 26, 2023 at 11:38 AM
-- Server version: 5.7.24
-- PHP Version: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `unitydatabase`
--

-- --------------------------------------------------------

--
-- Table structure for table `friends`
--

CREATE TABLE `friends` (
  `FriendID` int(11) NOT NULL,
  `ID` int(11) NOT NULL,
  `FriendshipID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `friends`
--

INSERT INTO `friends` (`FriendID`, `ID`, `FriendshipID`) VALUES
(20, 19, 1),
(19, 20, 2),
(20, 29, 3),
(29, 20, 4),
(19, 30, 5),
(30, 19, 6),
(22, 19, 7),
(19, 22, 8);

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `ItemID` int(11) NOT NULL,
  `ItemName` varchar(255) NOT NULL,
  `ItemIndex` int(11) NOT NULL,
  `ID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`ItemID`, `ItemName`, `ItemIndex`, `ID`) VALUES
(1, 'axe', 0, 19),
(2, 'axe', 20, 19),
(3, 'armor', 24, 19),
(4, 'armor', 3, 19),
(5, 'coins', 4, 19),
(6, 'coins', 5, 19),
(7, 'coins', 6, 19),
(8, 'coins', 7, 19),
(9, 'coins', 8, 19),
(10, 'boots', 9, 19),
(11, 'axe', 21, 29),
(12, 'armor', 1, 29),
(13, 'belts', 25, 29),
(14, 'armor', 0, 30),
(15, 'armor', 6, 30),
(16, 'armor', 12, 30),
(17, 'armor', 13, 30),
(18, 'armor', 4, 30),
(19, 'belts', 5, 30),
(20, 'book', 1, 30),
(21, 'axe', 6, 31),
(22, 'armor', 1, 19),
(23, 'axe', 2, 19),
(24, 'axe', 10, 19),
(25, 'axe', 11, 19);

-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE `players` (
  `ID` int(10) NOT NULL,
  `Username` varchar(16) NOT NULL,
  `Hash` varchar(100) NOT NULL,
  `Salt` varchar(100) NOT NULL,
  `Level` int(5) UNSIGNED NOT NULL DEFAULT '1',
  `Experience` int(15) UNSIGNED NOT NULL DEFAULT '0',
  `Health` int(15) NOT NULL DEFAULT '100',
  `MaxHealth` int(15) UNSIGNED NOT NULL DEFAULT '100',
  `Mana` int(15) NOT NULL DEFAULT '100',
  `MaxMana` int(15) UNSIGNED NOT NULL DEFAULT '100',
  `ProfilePicture` varchar(10) NOT NULL DEFAULT 'Warrior_1',
  `SkillPoint` int(5) UNSIGNED NOT NULL DEFAULT '0',
  `Coin` int(15) UNSIGNED NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `players`
--

INSERT INTO `players` (`ID`, `Username`, `Hash`, `Salt`, `Level`, `Experience`, `Health`, `MaxHealth`, `Mana`, `MaxMana`, `ProfilePicture`, `SkillPoint`, `Coin`) VALUES
(19, 'yusufagac', '$5$rounds=5000$steamedhamsyusuf$ATyaAUvaJYkKMZiRhx9aggREKZqGWrCoGvroyWN.9Q.', '$5$rounds=5000$steamedhamsyusufagac$', 6, 120, 35, 600, 302, 600, 'Samurai', 0, 275),
(20, 'xhakan23', '$5$rounds=5000$steamedhamsxhaka$//nxsDyqwciJyTQAfkNLUdE.PCZsW9usj9t2TTXo3U.', '$5$rounds=5000$steamedhamsxhakan23$', 1, 0, 100, 100, 100, 100, 'Werwolf', 0, 0),
(21, 'yusufyildirim', '$5$rounds=5000$steamedhamsyusuf$ATyaAUvaJYkKMZiRhx9aggREKZqGWrCoGvroyWN.9Q.', '$5$rounds=5000$steamedhamsyusufyildirim$', 1, 0, 100, 100, 100, 100, 'Warrior_1', 0, 0),
(22, 'erenturker', '$5$rounds=5000$steamedhamserent$y5kHaaO5ZzF81PpaWb19NOlliC9mcTqnNiB1XwYwkX3', '$5$rounds=5000$steamedhamserenturker$', 1, 0, 100, 100, 100, 100, 'Warrior_1', 0, 0),
(23, 'melihakay', '$5$rounds=5000$steamedhamsmelih$JWVCXN3TBQrX6m0VdHfEcfOjGBDbegC.QdDxmxf6rAD', '$5$rounds=5000$steamedhamsmelihakay$', 1, 0, 100, 100, 100, 100, 'Warrior_1', 0, 0),
(24, 'erenkallikci', '$5$rounds=5000$steamedhamserenk$rQ5pM4voY5OGb0gDVgQZIBMYfJUsT0af3GgHJJocrkA', '$5$rounds=5000$steamedhamserenkallikci$', 1, 0, 100, 100, 100, 100, 'Warrior_1', 0, 0),
(25, 'berkehazar', '$5$rounds=5000$steamedhamsberke$iUTEL2JrA26XdViv5IRI3.GFXJSo3ZFZ6yOQGfxO2uA', '$5$rounds=5000$steamedhamsberkehazar$', 1, 0, 100, 100, 100, 100, 'Warrior_1', 0, 0),
(26, 'muhammedkalabasi', '$5$rounds=5000$steamedhamsmuham$DcewAwYcZZM8YCCKKo8HAPxoWfnMwEtaO5CdXUERR74', '$5$rounds=5000$steamedhamsmuhammedkalabasi$', 1, 0, 100, 100, 100, 100, 'Warrior_1', 0, 0),
(27, 'ahmetcoban', '$5$rounds=5000$steamedhamsahmet$pxkGq4X861FBG0RIed4rO66pudjoy5CJENFcNq6RVS4', '$5$rounds=5000$steamedhamsahmetcoban$', 1, 0, 100, 100, 100, 100, 'Warrior_1', 0, 0),
(28, 'test1234', '$5$rounds=5000$steamedhamstest1$wLQnFbOuvmXkepwl3rrckh7z1w0g2NP8EzGgRxRPlq5', '$5$rounds=5000$steamedhamstest1234$', 1, 0, 100, 100, 100, 100, 'Warrior_1', 0, 0),
(29, 'deneme123', '$5$rounds=5000$steamedhamsdenem$0y/GEvhyO2xqoTTjx2QmvgUJs/jCJRgpw1plB2TSAdC', '$5$rounds=5000$steamedhamsdeneme123$', 2, 110, 79, 200, 76, 200, 'Warrior_1', 0, 1638),
(30, 'emirhan1234', '$5$rounds=5000$steamedhamsemirh$8HgOADpOfnlwBcYpzwWEzIxwY6SlU8tFTlWG9zIyCm7', '$5$rounds=5000$steamedhamsemirhan1234$', 4, 30, 79, 400, 60, 400, 'Troll', 0, 1385),
(31, 'muhammed', '$5$rounds=5000$steamedhamsmuham$xtf5/HdvpbXtkvUZ2qKRY4YxOxHUcQ2yKbp.ehrrAJ2', '$5$rounds=5000$steamedhamsmuhammed$', 4, 150, 100, 400, 100, 400, 'Warrior_1', 0, 3930);

-- --------------------------------------------------------

--
-- Table structure for table `skillbar`
--

CREATE TABLE `skillbar` (
  `SkillName` varchar(20) NOT NULL,
  `SkillBarIndex` int(11) NOT NULL,
  `ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `skillbar`
--

INSERT INTO `skillbar` (`SkillName`, `SkillBarIndex`, `ID`) VALUES
('Arcanist15', 0, 19),
('BeastMaster4', 2, 19),
('Barbarian14', 2, 29),
('Barbarian14', 0, 29),
('Cultist12', 3, 30),
('BeastMaster19', 2, 30),
('Arcanist15', 0, 30),
('Cultist2', 1, 31),
('BeastMaster4', 3, 31),
('Arcanist1', 0, 31),
('Cultist2', 2, 31),
('Barbarian20', 3, 19),
('BeastMaster4', 1, 19);

-- --------------------------------------------------------

--
-- Table structure for table `skills`
--

CREATE TABLE `skills` (
  `SkillName` varchar(15) NOT NULL,
  `ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `skills`
--

INSERT INTO `skills` (`SkillName`, `ID`) VALUES
('BeastMaster19', 19),
('Barbarian20', 19),
('Barbarian14', 29),
('BeastMaster19', 30),
('Cultist12', 30),
('Arcanist15', 30),
('BeastMaster4', 31),
('Cultist2', 31),
('Arcanist1', 31),
('Cultist12', 19),
('Arcanist15', 19),
('BeastMaster4', 19);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `friends`
--
ALTER TABLE `friends`
  ADD PRIMARY KEY (`FriendshipID`),
  ADD UNIQUE KEY `FriendshipID` (`FriendshipID`),
  ADD KEY `ID` (`ID`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`ItemID`),
  ADD KEY `ID` (`ID`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- Indexes for table `skillbar`
--
ALTER TABLE `skillbar`
  ADD KEY `ID` (`ID`);

--
-- Indexes for table `skills`
--
ALTER TABLE `skills`
  ADD KEY `ID` (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `friends`
--
ALTER TABLE `friends`
  MODIFY `FriendshipID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `ItemID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `players`
--
ALTER TABLE `players`
  MODIFY `ID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `friends`
--
ALTER TABLE `friends`
  ADD CONSTRAINT `friends_ibfk_1` FOREIGN KEY (`ID`) REFERENCES `players` (`ID`);

--
-- Constraints for table `items`
--
ALTER TABLE `items`
  ADD CONSTRAINT `items_ibfk_1` FOREIGN KEY (`ID`) REFERENCES `players` (`ID`);

--
-- Constraints for table `skillbar`
--
ALTER TABLE `skillbar`
  ADD CONSTRAINT `skillbar_ibfk_1` FOREIGN KEY (`ID`) REFERENCES `players` (`ID`);

--
-- Constraints for table `skills`
--
ALTER TABLE `skills`
  ADD CONSTRAINT `skills_ibfk_1` FOREIGN KEY (`ID`) REFERENCES `players` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
