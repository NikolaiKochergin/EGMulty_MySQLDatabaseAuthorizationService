<?php
    require 'RedbeanPHP/rb-mysql.php';

    R::setup('mysql:host=localhost;dbname=testdb', '<login>', '<password>');

    if(R::testConnection() == false){
        echo 'Connect Error';
        exit;
    }
?>