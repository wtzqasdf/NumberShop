-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- 主機： localhost
-- 產生時間： 2021-11-23 08:47:06
-- 伺服器版本： 10.4.16-MariaDB
-- PHP 版本： 7.3.24

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 資料庫： `numbershop`
--

-- --------------------------------------------------------

--
-- 資料表結構 `cart`
--

CREATE TABLE `cart` (
  `account` varchar(30) COLLATE utf8mb4_bin NOT NULL,
  `merchID` tinytext COLLATE utf8mb4_bin NOT NULL,
  `count` smallint(5) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- --------------------------------------------------------

--
-- 資料表結構 `coupon`
--

CREATE TABLE `coupon` (
  `couponID` tinytext COLLATE utf8mb4_bin NOT NULL,
  `couponType` tinytext COLLATE utf8mb4_bin NOT NULL,
  `couponName` tinytext COLLATE utf8mb4_bin NOT NULL,
  `percent` decimal(10,3) UNSIGNED DEFAULT NULL,
  `price` decimal(10,0) UNSIGNED DEFAULT NULL,
  `remainCount` mediumint(9) NOT NULL,
  `expireDate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- --------------------------------------------------------

--
-- 資料表結構 `merchdetail`
--

CREATE TABLE `merchdetail` (
  `merchID` varchar(500) COLLATE utf8mb4_bin NOT NULL,
  `typeID` tinytext COLLATE utf8mb4_bin NOT NULL,
  `name` tinytext COLLATE utf8mb4_bin NOT NULL,
  `description` tinytext COLLATE utf8mb4_bin NOT NULL,
  `price` decimal(10,0) UNSIGNED NOT NULL,
  `remain` smallint(5) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- --------------------------------------------------------

--
-- 資料表結構 `merchorder`
--

CREATE TABLE `merchorder` (
  `orderID` tinytext COLLATE utf8mb4_bin NOT NULL,
  `account` varchar(30) COLLATE utf8mb4_bin NOT NULL,
  `merchID` tinytext COLLATE utf8mb4_bin NOT NULL,
  `merchName` tinytext COLLATE utf8mb4_bin NOT NULL,
  `count` smallint(5) UNSIGNED NOT NULL,
  `originPrice` decimal(10,0) UNSIGNED NOT NULL,
  `couponID` tinytext COLLATE utf8mb4_bin NOT NULL,
  `orderTime` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- --------------------------------------------------------

--
-- 資料表結構 `merchtype`
--

CREATE TABLE `merchtype` (
  `typeID` tinytext COLLATE utf8mb4_bin NOT NULL,
  `name` tinytext COLLATE utf8mb4_bin NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- --------------------------------------------------------

--
-- 資料表結構 `review`
--

CREATE TABLE `review` (
  `merchID` tinytext COLLATE utf8mb4_bin NOT NULL,
  `account` varchar(30) COLLATE utf8mb4_bin NOT NULL,
  `score` tinyint(3) UNSIGNED NOT NULL,
  `content` text COLLATE utf8mb4_bin NOT NULL,
  `postdate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- --------------------------------------------------------

--
-- 資料表結構 `session`
--

CREATE TABLE `session` (
  `token` text COLLATE utf8mb4_bin NOT NULL,
  `account` varchar(30) COLLATE utf8mb4_bin NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- --------------------------------------------------------

--
-- 資料表結構 `user`
--

CREATE TABLE `user` (
  `account` varchar(30) COLLATE utf8mb4_bin NOT NULL,
  `password` text COLLATE utf8mb4_bin NOT NULL,
  `email` tinytext COLLATE utf8mb4_bin NOT NULL,
  `identity` tinytext COLLATE utf8mb4_bin NOT NULL,
  `permission` tinyint(3) UNSIGNED NOT NULL,
  `regdate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- --------------------------------------------------------

--
-- 資料表結構 `usercoupon`
--

CREATE TABLE `usercoupon` (
  `account` varchar(30) COLLATE utf8mb4_bin NOT NULL,
  `couponID` tinytext COLLATE utf8mb4_bin NOT NULL,
  `isUsed` tinyint(1) NOT NULL,
  `gotDate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

--
-- 已傾印資料表的索引
--

--
-- 資料表索引 `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`account`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
