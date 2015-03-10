/*
Navicat MySQL Data Transfer

Source Server         : localhostMYSQL
Source Server Version : 50623
Source Host           : localhost:3306
Source Database       : ryanstaurant

Target Server Type    : MYSQL
Target Server Version : 50623
File Encoding         : 65001

Date: 2015-03-10 18:18:34
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for authority
-- ----------------------------
DROP TABLE IF EXISTS `authority`;
CREATE TABLE `authority` (
  `id` int(11) NOT NULL,
  `NAME` varchar(50) NOT NULL,
  `DESCRIPTION` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of authority
-- ----------------------------
INSERT INTO `authority` VALUES ('1', 'PrintCheck', '');
INSERT INTO `authority` VALUES ('2', 'PrintReport', '');

-- ----------------------------
-- Table structure for dishes
-- ----------------------------
DROP TABLE IF EXISTS `dishes`;
CREATE TABLE `dishes` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `ShortCall` varchar(10) DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `Price` decimal(10,0) DEFAULT '0',
  `Cost` decimal(10,0) DEFAULT '0',
  `MainType` varchar(20) DEFAULT NULL,
  `SubType` varchar(20) DEFAULT NULL,
  `Photo` blob,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of dishes
-- ----------------------------

-- ----------------------------
-- Table structure for employee
-- ----------------------------
DROP TABLE IF EXISTS `employee`;
CREATE TABLE `employee` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(100) NOT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of employee
-- ----------------------------
INSERT INTO `employee` VALUES ('1', 'RYAN', '123456', 'DEFAULT ADMINISTRATOR');

-- ----------------------------
-- Table structure for emp_auth
-- ----------------------------
DROP TABLE IF EXISTS `emp_auth`;
CREATE TABLE `emp_auth` (
  `EMP_ID` int(11) NOT NULL,
  `AUTH_ID` int(11) NOT NULL,
  PRIMARY KEY (`EMP_ID`,`AUTH_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of emp_auth
-- ----------------------------

-- ----------------------------
-- Table structure for emp_role
-- ----------------------------
DROP TABLE IF EXISTS `emp_role`;
CREATE TABLE `emp_role` (
  `EMP_ID` int(11) NOT NULL,
  `ROLE_ID` int(11) NOT NULL,
  PRIMARY KEY (`EMP_ID`,`ROLE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of emp_role
-- ----------------------------

-- ----------------------------
-- Table structure for menu
-- ----------------------------
DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `MType` varchar(100) NOT NULL,
  `DishID` int(11) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `SubDescription` varchar(500) DEFAULT NULL,
  `SuiteID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of menu
-- ----------------------------

-- ----------------------------
-- Table structure for navigator
-- ----------------------------
DROP TABLE IF EXISTS `navigator`;
CREATE TABLE `navigator` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ParentID` int(11) DEFAULT NULL,
  `Label` varchar(100) NOT NULL,
  `ClassName` varchar(200) DEFAULT NULL,
  `AuthorityID` int(11) DEFAULT NULL,
  `SortNumber` int(11) DEFAULT NULL,
  `PicURL` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of navigator
-- ----------------------------
INSERT INTO `navigator` VALUES ('1', null, 'Restaurant', null, null, '1', null);
INSERT INTO `navigator` VALUES ('3', '1', 'FloorLayout', null, '1', '1', null);

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `description` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES ('1', 'Administrator', 'Administrator');
INSERT INTO `role` VALUES ('2', 'Manager', 'Manager');
INSERT INTO `role` VALUES ('3', 'Leader', 'Leader');
INSERT INTO `role` VALUES ('4', 'Server', 'Server');
INSERT INTO `role` VALUES ('5', 'Cashier', 'Cashier');
INSERT INTO `role` VALUES ('6', 'Cooker', 'Cooker');
INSERT INTO `role` VALUES ('7', 'Chef', 'Chef');

-- ----------------------------
-- Table structure for role_auth
-- ----------------------------
DROP TABLE IF EXISTS `role_auth`;
CREATE TABLE `role_auth` (
  `ROLE_ID` int(11) NOT NULL,
  `AUTH_ID` int(11) NOT NULL,
  PRIMARY KEY (`ROLE_ID`,`AUTH_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of role_auth
-- ----------------------------

-- ----------------------------
-- Table structure for seatimageconfig
-- ----------------------------
DROP TABLE IF EXISTS `seatimageconfig`;
CREATE TABLE `seatimageconfig` (
  `Direction` varchar(20) NOT NULL,
  `Status` varchar(20) NOT NULL,
  `PicURL` varchar(200) NOT NULL,
  PRIMARY KEY (`Direction`,`Status`,`PicURL`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of seatimageconfig
-- ----------------------------
INSERT INTO `seatimageconfig` VALUES ('North', 'Vocation', '1');

-- ----------------------------
-- Table structure for seatlist
-- ----------------------------
DROP TABLE IF EXISTS `seatlist`;
CREATE TABLE `seatlist` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `No` int(11) NOT NULL,
  `Direction` int(11) NOT NULL DEFAULT '0',
  `Width` int(10) NOT NULL DEFAULT '0',
  `Length` int(10) NOT NULL,
  `PosX` int(11) NOT NULL,
  `PosY` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of seatlist
-- ----------------------------
INSERT INTO `seatlist` VALUES ('1', '1', '0', '50', '50', '70', '100');

-- ----------------------------
-- Table structure for sysconfig
-- ----------------------------
DROP TABLE IF EXISTS `sysconfig`;
CREATE TABLE `sysconfig` (
  `ShortCall` varchar(200) NOT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `ConfigValue` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ShortCall`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sysconfig
-- ----------------------------

-- ----------------------------
-- Table structure for tableimageconfig
-- ----------------------------
DROP TABLE IF EXISTS `tableimageconfig`;
CREATE TABLE `tableimageconfig` (
  `Status` varchar(20) NOT NULL,
  `PicURL` varchar(200) NOT NULL,
  PRIMARY KEY (`Status`,`PicURL`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tableimageconfig
-- ----------------------------
INSERT INTO `tableimageconfig` VALUES ('Vocation', '2');

-- ----------------------------
-- Table structure for tablelist
-- ----------------------------
DROP TABLE IF EXISTS `tablelist`;
CREATE TABLE `tablelist` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `No` int(11) NOT NULL,
  `PosX` int(10) NOT NULL DEFAULT '0',
  `PosY` int(10) NOT NULL DEFAULT '0',
  `Width` int(10) NOT NULL DEFAULT '0',
  `Length` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tablelist
-- ----------------------------
INSERT INTO `tablelist` VALUES ('1', '1', '100', '100', '100', '100');
INSERT INTO `tablelist` VALUES ('2', '2', '200', '100', '100', '100');

-- ----------------------------
-- Table structure for table_seats
-- ----------------------------
DROP TABLE IF EXISTS `table_seats`;
CREATE TABLE `table_seats` (
  `TableID` int(11) NOT NULL,
  `SeatID` int(11) NOT NULL,
  `CurrentStatus` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of table_seats
-- ----------------------------
INSERT INTO `table_seats` VALUES ('1', '1', '0');

-- ----------------------------
-- View structure for v_employeeauthorities
-- ----------------------------
DROP VIEW IF EXISTS `v_employeeauthorities`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER  VIEW `v_employeeauthorities` AS SELECT
	id,
	`name`,
	`PASSWORD`,
	`description`,
	SUM(AUTH_ID) AS 'Authorities'
FROM
	v_employeeauthoritylist
GROUP BY
	id,
	`name`,
	`PASSWORD`,
	`description` ;

-- ----------------------------
-- View structure for v_employeeauthoritylist
-- ----------------------------
DROP VIEW IF EXISTS `v_employeeauthoritylist`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost`  VIEW `v_employeeauthoritylist` AS SELECT
	employee.*, emp_auth.AUTH_ID
FROM
	ryanstaurant.employee
LEFT JOIN emp_auth ON employee.id = emp_auth.EMP_ID
UNION
	SELECT
		te.*, tra.auth_id
	FROM
		ryanstaurant.employee AS te
	LEFT JOIN emp_role AS ter ON te.id = ter.emp_id
	LEFT JOIN role_auth AS tra ON ter.role_id = tra.role_id ;

-- ----------------------------
-- View structure for v_tableseats
-- ----------------------------
DROP VIEW IF EXISTS `v_tableseats`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost`  VIEW `v_tableseats` AS SELECT
	ts.TableID,
	t.`No` as TableNo,
	t.PosX as TablePositionX,
	t.PosY as TablePositionY,
	t.Width as TableWidth,
	t.Length as TableLength,
	ts.SeatID,
	s.`No` as SeatNo,
	s.Direction,
	s.Length as SeatLength,
	s.Width as SeatWidth,
	s.PosX as SeatPositionX,
	s.PosY as SeatPositionY,
	ts.CurrentStatus
FROM
	tablelist t
LEFT JOIN table_seats ts ON t.ID = ts.TableID
LEFT JOIN seatlist s ON ts.SeatID = s.ID ;
