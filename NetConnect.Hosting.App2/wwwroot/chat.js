"use strict";

function chatLoad(userId) {
    var messageInput = document.getElementById('message');
    var uri = '/chat';
    var broadcastMessageUri = 'broadcastMessage';
    var onlineUsersUi = 'onlineUsers';
    var sendUri = 'send';
    var name = 'App1';
    var selectedUser = $('#selectedUserId');
    var selectedUserText = $('#selectedUserText');

    messageInput.focus();

    var connection = new signalR.HubConnectionBuilder()
        .withUrl(uri)
        .build();

    connection.on(broadcastMessageUri, function (sendUserId, name, message) {
        var incomingMessageElement = document.createElement('div');
        if (sendUserId == userId) {
            incomingMessageElement.innerHTML = receiveMessage(false, message);
        }
        else {
            incomingMessageElement.innerHTML = receiveMessage(true, message);
        }
        document.getElementById('messages').appendChild(incomingMessageElement);
    });

    connection.on(onlineUsersUi, function (users) {
        console.log(users);
        $("#users").empty();

        users.forEach(function (entry) {
            console.log(entry);

            if (entry.userId != userId) {

                var liElement = document.createElement('div');

                liElement.innerHTML = appendOnlineUsers(userId, entry.nameLastname);

                document.getElementById('users').appendChild(liElement);

                $('.userList').on('click', function () {
                    $('#msgBox').css("display", "unset");
                    selectedUserText.text($(this).text());
                    selectedUser.val($(this).attr('id'));
                });
            }

        });
    });


    // Transport fallback functionality is now built into start.
    connection.start()
        .then(function () {

            document.getElementById('sendmessage').addEventListener('click', function (event) {
                // Call the Send method on the hub.
                var userId = selectedUser.val();

                connection.invoke(sendUri, name, messageInput.value, userId);

                // Clear text box and reset focus for next comment.
                messageInput.value = '';
                messageInput.focus();
                event.preventDefault();
            });

        })
        .catch(error => {
            console.error(error.message);
        });
}

function receiveMessage(isIncoming, message) {

    var result = '';

    if (isIncoming) {
        result = '<div class="incoming_msg">' +
            '<div class="received_msg">' +
            '<div class="received_withd_msg">' +
            '<p>' +
            message +
            '</p>' +
            '<span class="time_date"> </span>' +
            '</div>' +
            '</div >' +
            '</div >';
    }
    else {
        result = '<div class="outgoing_msg">' +
            '<div class="sent_msg">' +
            '<p>' +
            message +
            '</p>' +
            '<span class="time_date"></span>' +
            '</div >' +
            '</div >';
    }
    return result;
}

function appendOnlineUsers(userId, nameLastname) {
    return '<div class="chat_list active_chat">' +
        '<div class="chat_people">' +
        '<div class="chat_ib">' +
        '<a id="' + userId + '" href = "#" class="userList" > ' + nameLastname + '</a > ' +
        '<p>' +
        'Çevrimiçi' +
        ' ' +
        '</p>' +
        '</div>' +
        '</div>' +
        '</div>';
}