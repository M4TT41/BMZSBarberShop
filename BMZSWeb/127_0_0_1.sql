-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Már 13. 11:38
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.1.17

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

-- 
-- Adatbázis: `bmzsbarbershop`
--
CREATE DATABASE IF NOT EXISTS `bmzsbarbershop` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `bmzsbarbershop`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `fodrasz`
--

CREATE TABLE `fodrasz` (
  `Id` int(11) NOT NULL,
  `Nev` varchar(255) NOT NULL,
  `TelefonSzam` varchar(13) NOT NULL,
  `Leiras` varchar(255) NOT NULL,
  `PfpName` varchar(255) NOT NULL,
  `FodraszatId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `fodrasz`
--

INSERT INTO `fodrasz` (`Id`, `Nev`, `TelefonSzam`, `Leiras`, `PfpName`, `FodraszatId`) VALUES
(1, 'Kiss Péter', '+36 30 123 45', 'Mesterfodrász', 'peter.jpg', 1),
(2, 'Nagy Anna', '+36 70 234 56', 'Stylist', 'anna.jpg', 2),
(3, 'Tóth Gábor', '+36 20 345 67', 'Borbély', 'gabor.jpg', 3),
(4, 'Szabó Júlia', '+36 30 456 78', 'Női fodrász', 'julia.jpg', 4),
(5, 'Varga Dániel', '+36 70 567 89', 'Modern hajvágás specialistája', 'daniel.jpg', 5);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `fodraszat`
--

CREATE TABLE `fodraszat` (
  `Id` int(11) NOT NULL,
  `VarosId` int(11) NOT NULL,
  `Utca` varchar(255) NOT NULL,
  `HazSzam` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `fodraszat`
--

INSERT INTO `fodraszat` (`Id`, `VarosId`, `Utca`, `HazSzam`) VALUES
(1, 1, 'Fő utca', 10),
(2, 2, 'Deák tér', 5),
(3, 3, 'Kossuth utca', 22),
(4, 4, 'Petőfi utca', 7),
(5, 5, 'Bartók Béla út', 15);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `foglalas`
--

CREATE TABLE `foglalas` (
  `Id` int(11) NOT NULL,
  `NaptarId` int(11) NOT NULL,
  `VasarloId` int(11) NOT NULL,
  `SzolgaltatasId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `foglalas`
--

INSERT INTO `foglalas` (`Id`, `NaptarId`, `VasarloId`, `SzolgaltatasId`) VALUES
(1, 1, 1, 1),
(2, 2, 2, 3),
(3, 3, 3, 5);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kepek`
--

CREATE TABLE `kepek` (
  `Id` int(11) NOT NULL,
  `FodraszId` int(11) NOT NULL,
  `EleresiUt` varchar(255) NOT NULL,
  `KepNev` varchar(255) NOT NULL,
  `Leiras` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `kepek`
--

INSERT INTO `kepek` (`Id`, `FodraszId`, `EleresiUt`, `KepNev`, `Leiras`) VALUES
(1, 1, '/img/haj1.jpg', 'Klasszikus férfi vágás', 'Stílusos és letisztult'),
(2, 2, '/img/haj2.jpg', 'Modern női frizura', 'Elegáns és trendi'),
(3, 3, '/img/haj3.jpg', 'Borotvált fej', 'Letisztult és karakteres');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szolgaltatas`
--

CREATE TABLE `szolgaltatas` (
  `Id` int(11) NOT NULL,
  `SzolgaltatasNev` varchar(255) NOT NULL,
  `Idotartalom` int(11) NOT NULL COMMENT 'Percben megadva',
  `Ar` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `szolgaltatas`
--

INSERT INTO `szolgaltatas` (`Id`, `SzolgaltatasNev`, `Idotartalom`, `Ar`) VALUES
(1, 'Férfi vágás', 30, 5000),
(2, 'Női hajvágás', 45, 7000),
(3, 'Borotválás', 20, 3000),
(4, 'Hajfestés', 60, 10000),
(5, 'Gyermek hajvágás', 20, 3500);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `varos`
--

CREATE TABLE `varos` (
  `Id` int(11) NOT NULL,
  `IranyitoSzam` int(4) NOT NULL,
  `TelepulesNev` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `varos`
--

INSERT INTO `varos` (`Id`, `IranyitoSzam`, `TelepulesNev`) VALUES
(1, 1011, 'Budapest'),
(2, 9400, 'Sopron'),
(3, 9021, 'Győr'),
(4, 6720, 'Szeged'),
(5, 4032, 'Debrecen');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `vasarlo`
--

CREATE TABLE `vasarlo` (
  `Id` int(11) NOT NULL,
  `FelhasznaloNev` varchar(255) NOT NULL,
  `Nev` varchar(255) NOT NULL,
  `TelefonSzam` varchar(13) NOT NULL,
  `EMail` varchar(255) NOT NULL,
  `JelszoHash` blob NOT NULL,
  `JelszoSalt` blob NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `vasarlo`
--

INSERT INTO `vasarlo` (`Id`, `FelhasznaloNev`, `Nev`, `TelefonSzam`, `EMail`, `JelszoHash`, `JelszoSalt`) VALUES
(1, 'kovacsj', 'Kovács János', '+36 30 112 22', 'janos@gmail.com', 0x6861736831, 0x73616c7431),
(2, 'szaboe', 'Szabó Erika', '+36 70 223 33', 'erika@gmail.com', 0x6861736832, 0x73616c7432),
(3, 'kissp', 'Kiss Péter', '+36 20 334 44', 'peter@gmail.com', 0x6861736833, 0x73616c7433);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `fodrasz`
--
ALTER TABLE `fodrasz`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FodraszatId` (`FodraszatId`);

--
-- A tábla indexei `fodraszat`
--
ALTER TABLE `fodraszat`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `VarosId` (`VarosId`);

--
-- A tábla indexei `foglalas`
--
ALTER TABLE `foglalas`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `kepek`
--
ALTER TABLE `kepek`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `szolgaltatas`
--
ALTER TABLE `szolgaltatas`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `varos`
--
ALTER TABLE `varos`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `vasarlo`
--
ALTER TABLE `vasarlo`
  ADD PRIMARY KEY (`Id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `fodrasz`
--
ALTER TABLE `fodrasz`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT a táblához `fodraszat`
--
ALTER TABLE `fodraszat`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT a táblához `foglalas`
--
ALTER TABLE `foglalas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `kepek`
--
ALTER TABLE `kepek`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `szolgaltatas`
--
ALTER TABLE `szolgaltatas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT a táblához `varos`
--
ALTER TABLE `varos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT a táblához `vasarlo`
--
ALTER TABLE `vasarlo`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `fodrasz`
--
ALTER TABLE `fodrasz`
  ADD CONSTRAINT `fodrasz_ibfk_1` FOREIGN KEY (`FodraszatId`) REFERENCES `fodraszat` (`Id`);

--
-- Megkötések a táblához `fodraszat`
--
ALTER TABLE `fodraszat`
  ADD CONSTRAINT `fodraszat_ibfk_1` FOREIGN KEY (`VarosId`) REFERENCES `varos` (`Id`);
--
-- Adatbázis: `brawlers`
--
CREATE DATABASE IF NOT EXISTS `brawlers` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `brawlers`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `brawler`
--

CREATE TABLE `brawler` (
  `id` int(6) NOT NULL,
  `name` varchar(30) NOT NULL,
  `type` varchar(30) NOT NULL,
  `speed` int(4) NOT NULL,
  `weapon` varchar(30) NOT NULL,
  `health` int(5) NOT NULL,
  `popularity` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- A tábla adatainak kiíratása `brawler`
--

INSERT INTO `brawler` (`id`, `name`, `type`, `speed`, `weapon`, `health`, `popularity`) VALUES
(1, 'Shelly', 'Damage Dealer', 720, 'Shotgun', 2800, 80),
(2, 'Colt', 'Damage Dealer', 800, 'Pistols', 2400, 75),
(3, 'Bull', 'Tank', 600, 'Shotgun', 6000, 85),
(4, 'Jessie', 'Support', 720, 'Shooter', 3200, 70),
(5, 'Brock', 'Damage Dealer', 780, 'Rocket Launcher', 2400, 78),
(6, 'Darryl', 'Tank', 760, 'Shotgun', 4000, 70),
(7, 'Poco', 'Support', 740, 'Guitar', 3500, 72),
(8, 'Rico', 'Damage Dealer', 720, 'Bouncy Bullets', 2600, 65),
(9, 'Bibi', 'Tank', 720, 'Baseball Bat', 4000, 68),
(10, 'Emz', 'Damage Dealer', 740, 'Spray', 3000, 80),
(11, 'Nita', 'Support', 750, 'Bear', 3800, 76),
(12, 'Frank', 'Tank', 600, 'Hammer', 5000, 67),
(13, 'Pam', 'Support', 720, 'Gatling Gun', 4200, 69),
(14, 'Barley', 'Damage Dealer', 720, 'Bottles', 2800, 66),
(15, 'Tick', 'Damage Dealer', 780, 'Mines', 2500, 60),
(16, 'Leon', 'Assassin', 800, 'Revolver', 2800, 90),
(17, 'Spike', 'Damage Dealer', 720, 'Cactus', 2400, 88),
(18, 'Surge', 'Assassin', 720, 'Laser', 3200, 73),
(19, 'Gale', 'Support', 760, 'Blasts', 3000, 74),
(20, 'Crow', 'Assassin', 800, 'Poisoned Daggers', 2400, 85);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `brawler`
--
ALTER TABLE `brawler`
  ADD PRIMARY KEY (`id`);
--
-- Adatbázis: `filmdiary`
--
CREATE DATABASE IF NOT EXISTS `filmdiary` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `filmdiary`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `movie`
--

CREATE TABLE `movie` (
  `id` int(11) NOT NULL,
  `title` varchar(255) NOT NULL,
  `year` int(11) NOT NULL,
  `genre` varchar(50) NOT NULL,
  `director` varchar(50) NOT NULL,
  `description` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `movie`
--

INSERT INTO `movie` (`id`, `title`, `year`, `genre`, `director`, `description`) VALUES
(1, 'The Thin Red Line', 1998, 'Drama', 'Terrence Malick', 'Adaptation of James Jones\' autobiographical 1962 novel, focusing on the conflict at Guadalcanal during the second World War.'),
(2, 'Fargo', 1998, 'Crime', 'Joel Coen', 'Minnesota car salesman Jerry Lundegaard\'s inept crime falls apart due to his and his henchmen\'s bungling and the persistent police work of the quite pregnant Marge Gunderson.'),
(3, 'Princess Mononoke', 1997, 'Animation', 'Hayao Miyazaki', 'On a journey to find the cure for a Tatarigami\'s curse, Ashitaka finds himself in the middle of a war between the forest gods and Tatara, a mining colony. In this quest he also meets San, the Mononoke Hime.'),
(4, 'Princess Bride', 1997, 'Fantasy', 'Rob Reiner', 'A bedridden boy\'s grandfather reads him the story of a farmboy-turned-pirate who encounters numerous obstacles, enemies and allies in his quest to be reunited with his true love.'),
(5, 'The Truman Show', 1998, 'Satire', 'Peter Weir', 'An insurance salesman begins to suspect that his whole life is actually some sort of reality TV show.'),
(6, 'Perfect Days', 2023, 'Drama', 'Wim Wenders', 'Hirayama cleans public toilets in Tokyo, lives his life in simplicity and daily tranquility. Some encounters also lead him to reflect on himself.');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `movielog`
--

CREATE TABLE `movielog` (
  `id` int(11) NOT NULL,
  `movieId` int(11) NOT NULL,
  `email` varchar(100) NOT NULL,
  `date` date NOT NULL,
  `rating` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `movielog`
--

INSERT INTO `movielog` (`id`, `movieId`, `email`, `date`, `rating`) VALUES
(1, 4, 'gj@vizsga.hu', '2025-03-11', 10),
(2, 6, 'gj@vizsga.hu', '2025-03-11', 7),
(3, 2, 'gj@vizsga.hu', '2025-03-11', 8);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `email` varchar(100) NOT NULL,
  `fullname` varchar(200) NOT NULL,
  `pwd` blob NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`email`, `fullname`, `pwd`) VALUES
('gj@vizsga.hu', 'Gipsz Jakab', 0x872432dc9b7d7f9298d8f67eddf7f93a),
('string', 'string', 0xb45cffe084dd3d20d928bee85e7b0f21),
('valami', 'valami', 0xa8f5f167f44f4964e6c998dee827110c);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `movie`
--
ALTER TABLE `movie`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `movielog`
--
ALTER TABLE `movielog`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_log_users` (`email`),
  ADD KEY `FK_log_movie` (`movieId`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`email`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `movie`
--
ALTER TABLE `movie`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT a táblához `movielog`
--
ALTER TABLE `movielog`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `movielog`
--
ALTER TABLE `movielog`
  ADD CONSTRAINT `FK_log_movie` FOREIGN KEY (`movieId`) REFERENCES `movie` (`id`),
  ADD CONSTRAINT `FK_log_users` FOREIGN KEY (`email`) REFERENCES `users` (`email`);
--
-- Adatbázis: `phpmyadmin`
--
CREATE DATABASE IF NOT EXISTS `phpmyadmin` DEFAULT CHARACTER SET utf8 COLLATE utf8_bin;
USE `phpmyadmin`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__bookmark`
--

CREATE TABLE `pma__bookmark` (
  `id` int(10) UNSIGNED NOT NULL,
  `dbase` varchar(255) NOT NULL DEFAULT '',
  `user` varchar(255) NOT NULL DEFAULT '',
  `label` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `query` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Bookmarks';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__central_columns`
--

CREATE TABLE `pma__central_columns` (
  `db_name` varchar(64) NOT NULL,
  `col_name` varchar(64) NOT NULL,
  `col_type` varchar(64) NOT NULL,
  `col_length` text DEFAULT NULL,
  `col_collation` varchar(64) NOT NULL,
  `col_isNull` tinyint(1) NOT NULL,
  `col_extra` varchar(255) DEFAULT '',
  `col_default` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Central list of columns';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__column_info`
--

CREATE TABLE `pma__column_info` (
  `id` int(5) UNSIGNED NOT NULL,
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `table_name` varchar(64) NOT NULL DEFAULT '',
  `column_name` varchar(64) NOT NULL DEFAULT '',
  `comment` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `mimetype` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `transformation` varchar(255) NOT NULL DEFAULT '',
  `transformation_options` varchar(255) NOT NULL DEFAULT '',
  `input_transformation` varchar(255) NOT NULL DEFAULT '',
  `input_transformation_options` varchar(255) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Column information for phpMyAdmin';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__designer_settings`
--

CREATE TABLE `pma__designer_settings` (
  `username` varchar(64) NOT NULL,
  `settings_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Settings related to Designer';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__export_templates`
--

CREATE TABLE `pma__export_templates` (
  `id` int(5) UNSIGNED NOT NULL,
  `username` varchar(64) NOT NULL,
  `export_type` varchar(10) NOT NULL,
  `template_name` varchar(64) NOT NULL,
  `template_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Saved export templates';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__favorite`
--

CREATE TABLE `pma__favorite` (
  `username` varchar(64) NOT NULL,
  `tables` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Favorite tables';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__history`
--

CREATE TABLE `pma__history` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `username` varchar(64) NOT NULL DEFAULT '',
  `db` varchar(64) NOT NULL DEFAULT '',
  `table` varchar(64) NOT NULL DEFAULT '',
  `timevalue` timestamp NOT NULL DEFAULT current_timestamp(),
  `sqlquery` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='SQL history for phpMyAdmin';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__navigationhiding`
--

CREATE TABLE `pma__navigationhiding` (
  `username` varchar(64) NOT NULL,
  `item_name` varchar(64) NOT NULL,
  `item_type` varchar(64) NOT NULL,
  `db_name` varchar(64) NOT NULL,
  `table_name` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Hidden items of navigation tree';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__pdf_pages`
--

CREATE TABLE `pma__pdf_pages` (
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `page_nr` int(10) UNSIGNED NOT NULL,
  `page_descr` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='PDF relation pages for phpMyAdmin';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__recent`
--

CREATE TABLE `pma__recent` (
  `username` varchar(64) NOT NULL,
  `tables` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Recently accessed tables';

--
-- A tábla adatainak kiíratása `pma__recent`
--

INSERT INTO `pma__recent` (`username`, `tables`) VALUES
('root', '[{\"db\":\"bmzsbarbershop\",\"table\":\"varos\"},{\"db\":\"bmzsbarbershop\",\"table\":\"fodraszat\"},{\"db\":\"bmzsbarbershop\",\"table\":\"fodrasz\"},{\"db\":\"filmdiary\",\"table\":\"users\"},{\"db\":\"filmdiary\",\"table\":\"movie\"},{\"db\":\"brawlers\",\"table\":\"brawler\"}]');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__relation`
--

CREATE TABLE `pma__relation` (
  `master_db` varchar(64) NOT NULL DEFAULT '',
  `master_table` varchar(64) NOT NULL DEFAULT '',
  `master_field` varchar(64) NOT NULL DEFAULT '',
  `foreign_db` varchar(64) NOT NULL DEFAULT '',
  `foreign_table` varchar(64) NOT NULL DEFAULT '',
  `foreign_field` varchar(64) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Relation table';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__savedsearches`
--

CREATE TABLE `pma__savedsearches` (
  `id` int(5) UNSIGNED NOT NULL,
  `username` varchar(64) NOT NULL DEFAULT '',
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `search_name` varchar(64) NOT NULL DEFAULT '',
  `search_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Saved searches';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__table_coords`
--

CREATE TABLE `pma__table_coords` (
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `table_name` varchar(64) NOT NULL DEFAULT '',
  `pdf_page_number` int(11) NOT NULL DEFAULT 0,
  `x` float UNSIGNED NOT NULL DEFAULT 0,
  `y` float UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Table coordinates for phpMyAdmin PDF output';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__table_info`
--

CREATE TABLE `pma__table_info` (
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `table_name` varchar(64) NOT NULL DEFAULT '',
  `display_field` varchar(64) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Table information for phpMyAdmin';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__table_uiprefs`
--

CREATE TABLE `pma__table_uiprefs` (
  `username` varchar(64) NOT NULL,
  `db_name` varchar(64) NOT NULL,
  `table_name` varchar(64) NOT NULL,
  `prefs` text NOT NULL,
  `last_update` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Tables'' UI preferences';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__tracking`
--

CREATE TABLE `pma__tracking` (
  `db_name` varchar(64) NOT NULL,
  `table_name` varchar(64) NOT NULL,
  `version` int(10) UNSIGNED NOT NULL,
  `date_created` datetime NOT NULL,
  `date_updated` datetime NOT NULL,
  `schema_snapshot` text NOT NULL,
  `schema_sql` text DEFAULT NULL,
  `data_sql` longtext DEFAULT NULL,
  `tracking` set('UPDATE','REPLACE','INSERT','DELETE','TRUNCATE','CREATE DATABASE','ALTER DATABASE','DROP DATABASE','CREATE TABLE','ALTER TABLE','RENAME TABLE','DROP TABLE','CREATE INDEX','DROP INDEX','CREATE VIEW','ALTER VIEW','DROP VIEW') DEFAULT NULL,
  `tracking_active` int(1) UNSIGNED NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Database changes tracking for phpMyAdmin';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__userconfig`
--

CREATE TABLE `pma__userconfig` (
  `username` varchar(64) NOT NULL,
  `timevalue` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `config_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='User preferences storage for phpMyAdmin';

--
-- A tábla adatainak kiíratása `pma__userconfig`
--

INSERT INTO `pma__userconfig` (`username`, `timevalue`, `config_data`) VALUES
('root', '2025-03-13 10:34:07', '{\"Console\\/Mode\":\"collapse\",\"lang\":\"hu\"}');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__usergroups`
--

CREATE TABLE `pma__usergroups` (
  `usergroup` varchar(64) NOT NULL,
  `tab` varchar(64) NOT NULL,
  `allowed` enum('Y','N') NOT NULL DEFAULT 'N'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='User groups with configured menu items';

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pma__users`
--

CREATE TABLE `pma__users` (
  `username` varchar(64) NOT NULL,
  `usergroup` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Users and their assignments to user groups';

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `pma__bookmark`
--
ALTER TABLE `pma__bookmark`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `pma__central_columns`
--
ALTER TABLE `pma__central_columns`
  ADD PRIMARY KEY (`db_name`,`col_name`);

--
-- A tábla indexei `pma__column_info`
--
ALTER TABLE `pma__column_info`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `db_name` (`db_name`,`table_name`,`column_name`);

--
-- A tábla indexei `pma__designer_settings`
--
ALTER TABLE `pma__designer_settings`
  ADD PRIMARY KEY (`username`);

--
-- A tábla indexei `pma__export_templates`
--
ALTER TABLE `pma__export_templates`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `u_user_type_template` (`username`,`export_type`,`template_name`);

--
-- A tábla indexei `pma__favorite`
--
ALTER TABLE `pma__favorite`
  ADD PRIMARY KEY (`username`);

--
-- A tábla indexei `pma__history`
--
ALTER TABLE `pma__history`
  ADD PRIMARY KEY (`id`),
  ADD KEY `username` (`username`,`db`,`table`,`timevalue`);

--
-- A tábla indexei `pma__navigationhiding`
--
ALTER TABLE `pma__navigationhiding`
  ADD PRIMARY KEY (`username`,`item_name`,`item_type`,`db_name`,`table_name`);

--
-- A tábla indexei `pma__pdf_pages`
--
ALTER TABLE `pma__pdf_pages`
  ADD PRIMARY KEY (`page_nr`),
  ADD KEY `db_name` (`db_name`);

--
-- A tábla indexei `pma__recent`
--
ALTER TABLE `pma__recent`
  ADD PRIMARY KEY (`username`);

--
-- A tábla indexei `pma__relation`
--
ALTER TABLE `pma__relation`
  ADD PRIMARY KEY (`master_db`,`master_table`,`master_field`),
  ADD KEY `foreign_field` (`foreign_db`,`foreign_table`);

--
-- A tábla indexei `pma__savedsearches`
--
ALTER TABLE `pma__savedsearches`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `u_savedsearches_username_dbname` (`username`,`db_name`,`search_name`);

--
-- A tábla indexei `pma__table_coords`
--
ALTER TABLE `pma__table_coords`
  ADD PRIMARY KEY (`db_name`,`table_name`,`pdf_page_number`);

--
-- A tábla indexei `pma__table_info`
--
ALTER TABLE `pma__table_info`
  ADD PRIMARY KEY (`db_name`,`table_name`);

--
-- A tábla indexei `pma__table_uiprefs`
--
ALTER TABLE `pma__table_uiprefs`
  ADD PRIMARY KEY (`username`,`db_name`,`table_name`);

--
-- A tábla indexei `pma__tracking`
--
ALTER TABLE `pma__tracking`
  ADD PRIMARY KEY (`db_name`,`table_name`,`version`);

--
-- A tábla indexei `pma__userconfig`
--
ALTER TABLE `pma__userconfig`
  ADD PRIMARY KEY (`username`);

--
-- A tábla indexei `pma__usergroups`
--
ALTER TABLE `pma__usergroups`
  ADD PRIMARY KEY (`usergroup`,`tab`,`allowed`);

--
-- A tábla indexei `pma__users`
--
ALTER TABLE `pma__users`
  ADD PRIMARY KEY (`username`,`usergroup`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `pma__bookmark`
--
ALTER TABLE `pma__bookmark`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `pma__column_info`
--
ALTER TABLE `pma__column_info`
  MODIFY `id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `pma__export_templates`
--
ALTER TABLE `pma__export_templates`
  MODIFY `id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `pma__history`
--
ALTER TABLE `pma__history`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `pma__pdf_pages`
--
ALTER TABLE `pma__pdf_pages`
  MODIFY `page_nr` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `pma__savedsearches`
--
ALTER TABLE `pma__savedsearches`
  MODIFY `id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- Adatbázis: `test`
--
CREATE DATABASE IF NOT EXISTS `test` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `test`;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
