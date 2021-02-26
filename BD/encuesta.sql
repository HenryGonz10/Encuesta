-- --------------------------------------------------------
-- Host:                         localhost
-- Versión del servidor:         5.7.24 - MySQL Community Server (GPL)
-- SO del servidor:              Win64
-- HeidiSQL Versión:             10.2.0.5599
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Volcando estructura para tabla encuesta.detalle_encuesta
CREATE TABLE IF NOT EXISTS `detalle_encuesta` (
  `iddetalle_encuesta` int(11) NOT NULL AUTO_INCREMENT,
  `idencuesta` int(11) DEFAULT NULL,
  `Nombre` varchar(45) COLLATE utf32_spanish_ci DEFAULT NULL,
  `Titulo` varchar(45) COLLATE utf32_spanish_ci DEFAULT NULL,
  `Requerido` bit(1) DEFAULT NULL,
  `Tipo` int(11) DEFAULT NULL,
  PRIMARY KEY (`iddetalle_encuesta`),
  KEY `fk_detalle_encuesta_encuesta_idx` (`idencuesta`),
  KEY `fk_detalle_encuesta_tipo_idx` (`Tipo`),
  CONSTRAINT `fk_detalle_encuesta_encuesta` FOREIGN KEY (`idencuesta`) REFERENCES `encuesta` (`idencuesta`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_detalle_encuesta_tipo` FOREIGN KEY (`Tipo`) REFERENCES `tipo` (`idTipo`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf32 COLLATE=utf32_spanish_ci;

-- Volcando datos para la tabla encuesta.detalle_encuesta: ~1 rows (aproximadamente)
/*!40000 ALTER TABLE `detalle_encuesta` DISABLE KEYS */;
INSERT INTO `detalle_encuesta` (`iddetalle_encuesta`, `idencuesta`, `Nombre`, `Titulo`, `Requerido`, `Tipo`) VALUES
	(7, 3, '1', '2', b'0', 1);
/*!40000 ALTER TABLE `detalle_encuesta` ENABLE KEYS */;

-- Volcando estructura para función encuesta.Encriptar
DELIMITER //
CREATE DEFINER=`root`@`localhost` FUNCTION `Encriptar`(contraseña VARCHAR(500)) RETURNS varchar(500) CHARSET utf32 COLLATE utf32_spanish_ci
BEGIN
	DECLARE encriptacion VARCHAR(500);
    SET encriptacion = HEX(AES_ENCRYPT(contraseña,'Devel'));
    RETURN encriptacion;
END//
DELIMITER ;

-- Volcando estructura para tabla encuesta.encuesta
CREATE TABLE IF NOT EXISTS `encuesta` (
  `idencuesta` int(11) NOT NULL AUTO_INCREMENT,
  `Titulo` varchar(45) COLLATE utf32_spanish_ci DEFAULT NULL,
  `Descripcion` varchar(500) COLLATE utf32_spanish_ci DEFAULT NULL,
  `Fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  `Estado` bit(1) DEFAULT b'1',
  `idusuario` int(11) DEFAULT NULL,
  PRIMARY KEY (`idencuesta`),
  KEY `fk_encuesta_usuario_idx` (`idusuario`),
  CONSTRAINT `fk_encuesta_usuario` FOREIGN KEY (`idusuario`) REFERENCES `usuario` (`idUser`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf32 COLLATE=utf32_spanish_ci;

-- Volcando datos para la tabla encuesta.encuesta: ~2 rows (aproximadamente)
/*!40000 ALTER TABLE `encuesta` DISABLE KEYS */;
INSERT INTO `encuesta` (`idencuesta`, `Titulo`, `Descripcion`, `Fecha`, `Estado`, `idusuario`) VALUES
	(1, 'adsf', 'adsfasf', '2021-02-26 03:25:09', b'0', 1),
	(3, '1', '2', '2021-02-26 05:27:41', b'1', 1);
/*!40000 ALTER TABLE `encuesta` ENABLE KEYS */;

-- Volcando estructura para procedimiento encuesta.Login
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `Login`(IN Usr NVARCHAR(500),
IN contraseña NVARCHAR(500),
OUT resultado INT)
BEGIN
	IF EXISTS(SELECT idUser FROM usuario WHERE User = Usr AND Pass = (SELECT Encriptar(contraseña)) AND Estado = true) THEN
		SET resultado = (SELECT idUser FROM usuario WHERE User = Usr AND Pass = (SELECT Encriptar(contraseña)) AND Estado = true);
	ELSE
		SET resultado = false;
    END IF;
END//
DELIMITER ;

-- Volcando estructura para tabla encuesta.resultado
CREATE TABLE IF NOT EXISTS `resultado` (
  `idresultado` int(11) NOT NULL AUTO_INCREMENT,
  `iddetalle_encuesta` int(11) DEFAULT NULL,
  `resultado` varchar(500) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idresultado`),
  KEY `fk_resultado_detalle_idx` (`iddetalle_encuesta`),
  CONSTRAINT `fk_resultado_detalle` FOREIGN KEY (`iddetalle_encuesta`) REFERENCES `detalle_encuesta` (`iddetalle_encuesta`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf32 COLLATE=utf32_spanish2_ci;

-- Volcando datos para la tabla encuesta.resultado: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `resultado` DISABLE KEYS */;
/*!40000 ALTER TABLE `resultado` ENABLE KEYS */;

-- Volcando estructura para tabla encuesta.tipo
CREATE TABLE IF NOT EXISTS `tipo` (
  `idTipo` int(11) NOT NULL AUTO_INCREMENT,
  `Tipo` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Estado` bit(1) DEFAULT NULL,
  PRIMARY KEY (`idTipo`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf32 COLLATE=utf32_spanish_ci;

-- Volcando datos para la tabla encuesta.tipo: ~3 rows (aproximadamente)
/*!40000 ALTER TABLE `tipo` DISABLE KEYS */;
INSERT INTO `tipo` (`idTipo`, `Tipo`, `Estado`) VALUES
	(1, 'Texto', b'1'),
	(2, 'Número', b'1'),
	(3, 'Fecha', b'1');
/*!40000 ALTER TABLE `tipo` ENABLE KEYS */;

-- Volcando estructura para tabla encuesta.usuario
CREATE TABLE IF NOT EXISTS `usuario` (
  `idUser` int(11) NOT NULL AUTO_INCREMENT,
  `User` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Pass` varchar(500) CHARACTER SET utf8 DEFAULT NULL,
  `Estado` bit(1) DEFAULT b'1',
  PRIMARY KEY (`idUser`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf32 COLLATE=utf32_spanish_ci;

-- Volcando datos para la tabla encuesta.usuario: ~1 rows (aproximadamente)
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` (`idUser`, `User`, `Pass`, `Estado`) VALUES
	(1, 'Admin', '83DE074CAEE0EDE4D672BF9B759A381670868F62FCB66BCCB9A70F7137BA0DC1', b'1');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
