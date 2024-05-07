<?php
    require '../database.php';

    $userID = $_POST['userID'];
    $selectedIDsJson = $_POST['selectedIDs'];

    if(!isset($userID) || !isset($selectedIDsJson)){
        echo 'Data struct error';
        exit;
    }

    $user = R::load('users', $userID);

    $selectedCardBeans = $user -> withCondition('cards_users.selected = ?', array(true)) -> ownCardsUsers;

    foreach($selectedCardBeans as $card){
        $card -> selected = false;
    }

    R::store($user);
    

    $selectedIDs = json_decode($selectedIDsJson, true)['IDs'];
    
    $links = $user -> withCondition('cards_users.cards_id IN ('. R::genSlots($selectedIDs) .')', $selectedIDs) -> ownCardsUsers;

    foreach($links as $link){
        $link -> selected = true;
    }

    R::store($user);

    echo 'ok';
?>