let chat = {
    signalRConnection: undefined,
    signalRConnectionStarted: undefined,
    init: function () {
        chat.signalRConnection = new signalR.HubConnectionBuilder().withUrl("/realTimeChatHub").build();
        chat.signalRConnectionStarted = chat.signalRConnection.start();
        chat.signalRConnection.on("NewRoomAdded", function (message) {
            chat.room.getAllRooms();
        });
        chat.signalRConnection.on("GetRoomMessage", function (message) {
            var activeRoomId = $('.list-group-item-action.active').data("roomid");
            if ((activeRoomId || "") != "") {
                chat.room.getMessagesByRoomId(activeRoomId);
            }
        });
        chat.room.getAllRooms();
    },
    room: {
        getAllRooms: function () {
            var activeRoomId = $('.list-group-item-action.active').data("roomid");
            $.post("/Home/_GetAllRooms", {
                selectedRoomId: activeRoomId
            }, function (res) {
                $('#chat-rooms-wrapper').html(res);
            });
        },
        getMessagesByRoomId: function (roomId) {
            $.post("/Home/_GetRoomMessages", { roomId: roomId }, function (res) {
                $('.chat-message-history').html(res);
            });
        },
        getRoomMessages: function (btn) {
            $('.list-group-item-action').removeClass("active");
            $(btn).addClass("active");
            var selectedRoom = $(btn).data("roomid");
            chat.room.getMessagesByRoomId(selectedRoom);
        },
        add: function () {

            Swal.fire({
                title: 'Please enter a room name',
                input: 'text',
                inputAttributes: {
                    autocapitalize: 'off'
                },
                showCancelButton: true,
                confirmButtonText: 'Create Room',
                showLoaderOnConfirm: true,
                preConfirm: (login) => {
                    if ((login || "").trim().length == 0) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Please enter a valid Room name!',
                        });
                        return;
                    }


                    return $.ajax({
                        url: "/Home/CreateRoom",
                        type: "POST",
                        data: { roomName: login },
                        success: function () {
                            chat.signalRConnection.invoke("SendNewRoomPush")
                            chat.room.getAllRooms();
                        },
                    })

                },
                allowOutsideClick: () => !Swal.isLoading()
            })
        },
        joinToRoom: function (btn) {
            var roomId = $(btn).data("roomid");
            Swal.fire({
                title: 'Are you sure?',
                text: "You are going to join the room!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Join it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Home/JoinToRoom",
                        type: "POST",
                        data: { roomId: roomId },
                        success: function () {
                            chat.room.getAllRooms();
                            chat.room.getMessagesByRoomId(roomId);
                        },
                    })
                }
            })


        }
    },
    message: {
        add: function () {
            var message = $('#message-from-input').val();
            var activeRoomId = $('.list-group-item-action.active').data("roomid");
            var joinned = $('.list-group-item-action.active').data("joinned");
            if (!joinned) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Please join to send message!',
                });
                return;
            }

            if ((message || "").length == 0 || (activeRoomId || "").length == 0) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Please enter a message and Choose a Room!',
                });
                return false;
            }


            $.ajax({
                url: "/Home/SaveMessage",
                type: "POST",
                data: {
                    message: message,
                    roomId: activeRoomId
                },
                success: function () {
                    chat.signalRConnection.invoke("SendNewMessagePush")
                    chat.room.getMessagesByRoomId(activeRoomId);
                },
                complete: function () {
                    $('#message-from-input').val("")

                }
            })

        }
    }
}

$(document).ready(function () {
    chat.init();
})