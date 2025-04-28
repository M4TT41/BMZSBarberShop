-- phpMyAdmin SQL Dump
-- version 4.7.1
-- https://www.phpmyadmin.net/
--
-- Gép: sql7.freesqldatabase.com
-- Létrehozás ideje: 2025. Ápr 28. 06:59
-- Kiszolgáló verziója: 5.5.62-0ubuntu0.14.04.1
-- PHP verzió: 7.0.33-0ubuntu0.16.04.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `bmzsbarbershop`
--
DROP DATABASE IF EXISTS `bmzsbarbershop`;
CREATE DATABASE IF NOT EXISTS `bmzsbarbershop` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `bmzsbarbershop`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Beosztas`
--

CREATE TABLE `Beosztas` (
  `Id` int(11) NOT NULL,
  `FodraszId` int(11) NOT NULL,
  `Datum` date NOT NULL,
  `NapNeve` enum('Hétfo','Kedd','Szerda','Csütörtök','Péntek','Szombat','Vasárnap') COLLATE utf8_hungarian_ci NOT NULL,
  `Kezdes` time NOT NULL,
  `Vege` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `Beosztas`
--

INSERT INTO `Beosztas` (`Id`, `FodraszId`, `Datum`, `NapNeve`, `Kezdes`, `Vege`) VALUES
(1, 1, '2025-04-29', 'Szerda', '12:30:00', '18:30:00');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Fodrasz`
--

CREATE TABLE `Fodrasz` (
  `Id` int(11) NOT NULL,
  `Nev` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `TelefonSzam` varchar(15) COLLATE utf8_hungarian_ci NOT NULL,
  `Leiras` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `PfpName` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `FodraszatId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `Fodrasz`
--

INSERT INTO `Fodrasz` (`Id`, `Nev`, `TelefonSzam`, `Leiras`, `PfpName`, `FodraszatId`) VALUES
(1, 'Robika', '06301791348', 'Férfi fodrász. Szakállvágást is vállal.', 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745781897/FodraszPfp/ttgyopogkoejnfntchbv.jpg', 5),
(3, 'Soós Benjámin', '+3630333333', 'Szakállvágó specialista', 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745782431/FodraszPfp/zahez33nopur5hnfhzss.jpg', 15),
(4, 'Gipsz Jakab', '06206541832', 'A hajfestés mestere', 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745782804/FodraszPfp/snaxrxxnjnpmn9k3xyyt.jpg', 16),
(9, 'Kovács János', '+3630333333', 'Hihetetlen pontos Borbély', 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745783160/FodraszPfp/kczd5evvtmtpj5xc4huz.jpg', 16),
(10, 'Hajba Martin', '06206541832', 'Fodrász tanonc. Gyakorlati munkát végez.', 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745782178/FodraszPfp/pritagzltvj3tomtobxu.jpg', 5),
(14, 'Jóska Pista', '+3630333333', 'Buzzcut specialista', 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745783306/FodraszPfp/hynkreyfc2f7jyctzcgo.jpg', 18),
(15, 'Kiss Pista', '+36303333333', 'Az olló és a hajnyírógép mestere', 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745783578/FodraszPfp/w1t80vsjrnf9ygdrhbzi.jpg', 18),
(16, 'Mataj', '06909489988', 'A festés a specialitása.', 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745782287/FodraszPfp/pwgf5vcxyuuyic4xfil9.jpg', 5),
(17, 'Vágó József', '+3630333333', 'A hajvágások mestere', 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745782685/FodraszPfp/bsczokxqjndxc0ejjwwe.jpg', 15);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Fodraszat`
--

CREATE TABLE `Fodraszat` (
  `Id` int(11) NOT NULL,
  `VarosId` int(11) NOT NULL,
  `Utca` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `HazSzam` int(11) NOT NULL,
  `FodraszatAzonHash` blob NOT NULL,
  `FodraszatAzonSalt` blob NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `Fodraszat`
--

INSERT INTO `Fodraszat` (`Id`, `VarosId`, `Utca`, `HazSzam`, `FodraszatAzonHash`, `FodraszatAzonSalt`) VALUES
(5, 6, 'Fekete István utca', 5, 0x5b1ad26a12b5f654f48af886d57ef6d2926694e7a87a1709d3bfb9b6b7765284, 0x759b4973434950172de741b25598dd8749572b77f7f46b4e012cd822def7d1e8904186af2eed7334bdffc69c0e6c812fad18a9f5c0d25f906b9cbf6f6a249267),
(15, 8, 'Néphadsereg utca', 17, 0xbd947f9ceef5b07e9337d1cc0dd21d118e3915e941beec474ac2184b82c15523, 0x5e39291e97c91add9f079f3195cf63d531c226cc874b8f94adeb72e9598ae3d23afb1ac84023bce1124be0b6126720ef36297dd0dbd75e6aaaaae1a51cdd9d61),
(16, 15, 'Zrínyi Ilona utca', 12, 0x0186cb3043bb705cb64c9830d0bc1a3f735d9075326ce633c7a81b5d417ea2d4, 0xf6931f160f9b36a4f6c3741ad726a1b43f73979ba6619aec37cb8294e20de07e2936d282b0b6aed683c7ddbbb662d39922b315c6b576506d2a001a88f22de324),
(18, 18, 'Kis', 74, 0x448c456fec6313754136c987c96dbbb28dcc633bdcc16a03a02b6743be3d04eb, 0xb878dac515af4639db7156f4912f4dc4f1047b2db0ae9ea9a6a65ae76f91d112f9065c34c9ad9c6312c33eb82175173083de785bb7b78cbc295755012c822848);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Foglalas`
--

CREATE TABLE `Foglalas` (
  `Id` int(11) NOT NULL,
  `NaptarId` int(11) NOT NULL,
  `VasarloId` int(11) NOT NULL,
  `SzolgaltatasId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Kepek`
--

CREATE TABLE `Kepek` (
  `Id` int(11) NOT NULL,
  `FodraszId` int(11) NOT NULL,
  `EleresiUt` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `KepNev` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `Leiras` varchar(255) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `Kepek`
--

INSERT INTO `Kepek` (`Id`, `FodraszId`, `EleresiUt`, `KepNev`, `Leiras`) VALUES
(1, 1, 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745784655/Kepek/gpeqo5k9nkcvfxovuzvy.webp', 'valami', 'Haj'),
(5, 14, 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745769174/Kepek/cgzcar9v4wgirvpyycoz.jpg', 'valami', 'Haj2'),
(6, 14, 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745769176/Kepek/jimnvgmakewhrtyr6x6f.jpg', 'valami', 'Haj3'),
(7, 9, 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745692402/Kepek/j5eblo2hs4a3ckku4x1j.jpg', 'valami', 'haj4'),
(8, 14, 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745769178/Kepek/dpud0geolznoki4p6npu.jpg', 'valami', 'haj5'),
(9, 9, 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745691402/Kepek/fgsyhedvxbktrqvmggec.jpg', 'valami', 'haj6'),
(10, 9, 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745692244/Kepek/zi92nvv3cciga5t4cerg.jpg', 'valami', 'kep7'),
(11, 9, 'https://res.cloudinary.com/bmzsbarbershop/image/upload/v1745692282/Kepek/zd8e3yoykfnoph8fngcu.jpg', 'valami', 'kep8');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Naptar`
--

CREATE TABLE `Naptar` (
  `Id` int(11) NOT NULL,
  `Datum` datetime NOT NULL,
  `FodraszId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `Naptar`
--

INSERT INTO `Naptar` (`Id`, `Datum`, `FodraszId`) VALUES
(94, '2025-04-30 09:00:00', 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Szavazott`
--

CREATE TABLE `Szavazott` (
  `Csillag` int(1) NOT NULL,
  `KepekId` int(11) DEFAULT NULL,
  `VasarloId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `Szavazott`
--

INSERT INTO `Szavazott` (`Csillag`, `KepekId`, `VasarloId`) VALUES
(1, 5, 10),
(4, 6, 10),
(5, 8, 10),
(1, 1, 10),
(3, 11, 10),
(4, 10, 10),
(3, 9, 10),
(1, 7, 10),
(5, 1, 5),
(1, 6, 5),
(5, 5, 5),
(2, 8, 5),
(3, 7, 5),
(3, 9, 5),
(1, 10, 5),
(4, 11, 5),
(4, 1, 11),
(4, 7, 11),
(3, 9, 11),
(2, 10, 11),
(3, 11, 11);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Szolgaltatas`
--

CREATE TABLE `Szolgaltatas` (
  `Id` int(11) NOT NULL,
  `SzolgaltatasNev` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `Idotartalom` int(11) NOT NULL COMMENT 'Percben megadva',
  `Ar` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `Szolgaltatas`
--

INSERT INTO `Szolgaltatas` (`Id`, `SzolgaltatasNev`, `Idotartalom`, `Ar`) VALUES
(7, 'Hajvágás', 30, 3500),
(8, 'Gyermek hajvágás', 30, 2500),
(9, 'Hosszú hajvágás', 30, 4500),
(10, 'Hajfestés', 30, 4000),
(11, 'Hajformázás', 30, 2000),
(12, 'Fejbor masszázs', 30, 2500),
(13, 'Konzultáció', 30, 1000),
(14, 'Borotválás', 30, 3000),
(15, 'Férfi arcpakolás', 30, 2000);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Varos`
--

CREATE TABLE `Varos` (
  `Id` int(11) NOT NULL,
  `IranyitoSzam` int(4) NOT NULL,
  `TelepulesNev` varchar(255) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `Varos`
--

INSERT INTO `Varos` (`Id`, `IranyitoSzam`, `TelepulesNev`) VALUES
(6, 9600, 'Sárvár'),
(8, 9794, 'Felsocsatár'),
(15, 9700, 'Szombathely'),
(18, 9970, 'Szentgotthárd');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Vasarlo`
--

CREATE TABLE `Vasarlo` (
  `Id` int(11) NOT NULL,
  `FelhasznaloNev` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `Nev` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `TelefonSzam` varchar(15) COLLATE utf8_hungarian_ci NOT NULL,
  `EMail` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `ProfilePic` varchar(255) COLLATE utf8_hungarian_ci NOT NULL,
  `JelszoHash` blob NOT NULL,
  `JelszoSalt` blob NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `Vasarlo`
--

INSERT INTO `Vasarlo` (`Id`, `FelhasznaloNev`, `Nev`, `TelefonSzam`, `EMail`, `ProfilePic`, `JelszoHash`, `JelszoSalt`) VALUES
(5, 'Robika', 'Zanati Zsolt', '+36 30 323 79', 'zzsolt05@gmail.com', '', 0x6387f7d24f59e0aba0e07059bd1477f916b83ca36c36e39d7da74e70ca0afc2f, 0x032026603a04ca5bd4b7deb0452ee52e4f596e353cadf9da2433d93221582598cf20af77c4160e4916cc07b36070922f5e3caf9b4e1ffe672f5cd29b9608c1b8),
(6, 'Robika', 'Zanati Zsolt', '+36303237997', 'zzsolt04@gmail.com', '', 0x8fff7bf8da4fbec27d4c7373601fefd28e327a5448cd9dc4b7ce4da9a90cb6c9, 0xda56b9edffcb2afd0bedf4485d4c5344be920fb38d0d8f649e860aea91ce4cdfa3583aba759efe8ef61f66772867d4b9919cecb9f64297da3f5679d457bcc182),
(8, 'Robika', 'Zanati Zsolt', '+36303237997', 'zzsolt@gmail.com', '', 0x50f0e11579b95f4998eb6406a5bbab7adb0ea3e992f3075d2994a59ce33ae68c, 0xbedec62bbaeea84b46baf2c8bf079cba59d63fbd7b956d44ae4b207fd797bfe53f51abdc6bfa1310cd782e8f701d017b07f4e86cbe02186643d536ef25f95dbb),
(9, 'Felhasznalo', 'Felhasznalo', '+36 30 323 7997', 'email@email.com', '', 0x6b7d1a0f52a55fb2a03eb0cf0e8e230ce2b14d596c4de69b584011ab35845aec, 0x7804bbede40baed5ee6661377f2c572163803a5cd38010c45a18b86d5d46b8bcd3e47913b12d5f4e8764d779495216ea762bc9049d88d23dbf2fe8e26a161a5c),
(10, 'TesztesJanika', 'Teszt János', '+36 30 323 7997', 'teszt@teszt.com', '', 0x9f7dfaf8581d8a59c3c1fb4f90e7311c701293dd0fec1168e6f19ce260b404cc, 0xd5138d4b7c0436ad289b66e16022b2930701280c2309d3e9db524ac76f498daec5482e0449a88a5015d6e5ef681f5debe89d803d7b3d8ef7463cc8a9a4b7d2e6),
(11, 'TesztesJani', 'Teszt János', '+36 30 323 7997', 'janos@teszt.com', 'qcmyjysptnvpphouqwdm', 0xead0adf56fd22b4e3d3e1fbf2494efca098d53803f7702b8db58e178d99c3091, 0x93604db18d65f75c9895575e0f6ec58335eaf4b9390400e9a205a15dff9a1e10c8002de9db6fd0d328a000d3ddd575956cc95ec8247e07a3eab0b94d7ce2b545);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `Beosztas`
--
ALTER TABLE `Beosztas`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FodraszId` (`FodraszId`);

--
-- A tábla indexei `Fodrasz`
--
ALTER TABLE `Fodrasz`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FodraszatId` (`FodraszatId`);

--
-- A tábla indexei `Fodraszat`
--
ALTER TABLE `Fodraszat`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Iranyitoszam` (`VarosId`);

--
-- A tábla indexei `Foglalas`
--
ALTER TABLE `Foglalas`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `NaptarId` (`NaptarId`),
  ADD KEY `VasarloId` (`VasarloId`),
  ADD KEY `SzolgaltatasId` (`SzolgaltatasId`);

--
-- A tábla indexei `Kepek`
--
ALTER TABLE `Kepek`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FodraszId` (`FodraszId`);

--
-- A tábla indexei `Naptar`
--
ALTER TABLE `Naptar`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FodraszId` (`FodraszId`);

--
-- A tábla indexei `Szavazott`
--
ALTER TABLE `Szavazott`
  ADD KEY `KepekId` (`KepekId`),
  ADD KEY `VasarloId` (`VasarloId`);

--
-- A tábla indexei `Szolgaltatas`
--
ALTER TABLE `Szolgaltatas`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `Varos`
--
ALTER TABLE `Varos`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `Vasarlo`
--
ALTER TABLE `Vasarlo`
  ADD PRIMARY KEY (`Id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `Beosztas`
--
ALTER TABLE `Beosztas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
--
-- AUTO_INCREMENT a táblához `Fodrasz`
--
ALTER TABLE `Fodrasz`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
--
-- AUTO_INCREMENT a táblához `Fodraszat`
--
ALTER TABLE `Fodraszat`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
--
-- AUTO_INCREMENT a táblához `Foglalas`
--
ALTER TABLE `Foglalas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=60;
--
-- AUTO_INCREMENT a táblához `Kepek`
--
ALTER TABLE `Kepek`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
--
-- AUTO_INCREMENT a táblához `Naptar`
--
ALTER TABLE `Naptar`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=95;
--
-- AUTO_INCREMENT a táblához `Szolgaltatas`
--
ALTER TABLE `Szolgaltatas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;
--
-- AUTO_INCREMENT a táblához `Varos`
--
ALTER TABLE `Varos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
--
-- AUTO_INCREMENT a táblához `Vasarlo`
--
ALTER TABLE `Vasarlo`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `Beosztas`
--
ALTER TABLE `Beosztas`
  ADD CONSTRAINT `Beosztas_ibfk_1` FOREIGN KEY (`FodraszId`) REFERENCES `Fodrasz` (`Id`);

--
-- Megkötések a táblához `Fodrasz`
--
ALTER TABLE `Fodrasz`
  ADD CONSTRAINT `Fodrasz_ibfk_1` FOREIGN KEY (`FodraszatId`) REFERENCES `Fodraszat` (`Id`);

--
-- Megkötések a táblához `Fodraszat`
--
ALTER TABLE `Fodraszat`
  ADD CONSTRAINT `Fodraszat_ibfk_1` FOREIGN KEY (`VarosId`) REFERENCES `Varos` (`Id`);

--
-- Megkötések a táblához `Foglalas`
--
ALTER TABLE `Foglalas`
  ADD CONSTRAINT `foglalas_ibfk_1` FOREIGN KEY (`VasarloId`) REFERENCES `Vasarlo` (`Id`),
  ADD CONSTRAINT `foglalas_ibfk_2` FOREIGN KEY (`NaptarId`) REFERENCES `Naptar` (`Id`),
  ADD CONSTRAINT `foglalas_ibfk_3` FOREIGN KEY (`SzolgaltatasId`) REFERENCES `Szolgaltatas` (`Id`);

--
-- Megkötések a táblához `Kepek`
--
ALTER TABLE `Kepek`
  ADD CONSTRAINT `kepek_ibfk_1` FOREIGN KEY (`FodraszId`) REFERENCES `Fodrasz` (`Id`);

--
-- Megkötések a táblához `Naptar`
--
ALTER TABLE `Naptar`
  ADD CONSTRAINT `naptar_ibfk_1` FOREIGN KEY (`FodraszId`) REFERENCES `Fodrasz` (`Id`);

--
-- Megkötések a táblához `Szavazott`
--
ALTER TABLE `Szavazott`
  ADD CONSTRAINT `szavazott_ibfk_1` FOREIGN KEY (`KepekId`) REFERENCES `Kepek` (`Id`),
  ADD CONSTRAINT `szavazott_ibfk_2` FOREIGN KEY (`VasarloId`) REFERENCES `Vasarlo` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
