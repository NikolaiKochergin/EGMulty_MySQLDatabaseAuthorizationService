<?php
    require '../database.php';

    $login = $_POST['login'];
    $password = $_POST['password'];

    if(!isset($login) || !isset($password)){
        echo 'Data struct error';
        exit;
    }

    $repeatCheker = R::findOne('users', 'login = ?', array($login));

    if(isset($repeatCheker)){
        echo 'Login reserved';
        exit;
    }

    $user = R::dispense('users');
    $user -> login = $login;
    $user -> password = $password;

    R::store($user);

    echo 'ok';
?>