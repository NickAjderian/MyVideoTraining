/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
var app = {
    // Application Constructor
    initialize: function() {
        this.bindEvents();
		
		//call PhonegapShim
        PhoneGap.init(function () {
            //alert("shimming up");
            var s = "Environment: "
                + (PhoneGap.env.browser ? "Browser " : "")
                + (PhoneGap.env.app ? "App " : "")
                + (PhoneGap.env.mobile ? "Mobile" : "");
					
            app.onDeviceReady();
        });
		
    },
    // Bind Event Listeners
    //
    // Bind any events that are required on startup. Common events are:
    // 'load', 'deviceready', 'offline', and 'online'.
    bindEvents: function() {
        document.addEventListener('deviceready', this.onDeviceReady, false);
    },
    // deviceready Event Handler
    //
    // The scope of 'this' is the event. In order to call the 'receivedEvent'
    // function, we must explicitly call 'app.receivedEvent(...);'
    onDeviceReady: function() {
        app.receivedEvent('deviceready');
		setTimeout(function(){
		    $('.app').hide();
		    $("#PanelShowLogin").show();
		},2000);
    },
    // Update DOM on a Received Event
    receivedEvent: function(id) {
        var parentElement = document.getElementById(id);
        var listeningElement = parentElement.querySelector('.listening');
        var receivedElement = parentElement.querySelector('.received');

        listeningElement.setAttribute('style', 'display:none;');
        receivedElement.setAttribute('style', 'display:block;');

        console.log('Received Event: ' + id);
    }
};

app.initialize();

var wrapper = document.getElementById("signature-pad"),
    clearButton = wrapper.querySelector("[data-action=clear]"),
    saveButton = wrapper.querySelector("[data-action=save]"),
    canvas = wrapper.querySelector("canvas");

// Adjust canvas coordinate space taking into account pixel ratio,
// to make it look crisp on mobile devices.
// This also causes canvas to be cleared.
function resizeCanvas() {
    // When zoomed out to less than 100%, for some very strange reason,
    // some browsers report devicePixelRatio as less than 1
    // and only part of the canvas is cleared then.
    var ratio =  Math.max(window.devicePixelRatio || 1, 1);
    canvas.width = canvas.offsetWidth * ratio;
    canvas.height = canvas.offsetHeight * ratio;
    canvas.getContext("2d").scale(ratio, ratio);
}

window.onresize = resizeCanvas;
resizeCanvas();

angular.module('app', []);
angular.module('app').controller('mainCtrl', function ($scope, $http) {
    $scope.status = 'ok so far';
    $scope.state = 'capturelogin';
    $scope.videoHasEnded = true;

    $scope.run = function(){
    };


    $scope.login = function () {
        //TODO: actually check credentials
        $scope.state = 'choosepatient';
        $scope.getPatients();
    };
    $scope.getPatients = function () {
        $http.get(APIURL + "candidates").then(function (data) {
            $scope.cands = JSON.parse(data.data).Result;
            //cands = [{ClientID, PersonName,Assignments:[{AssignmentID, AssignmentType { AssignmenttypeName}}]}]
        });
    };
    $scope.onSelectPerson = function(person){
        $scope.selectedPerson = person;
    }
    $scope.onChoosePatient = function () {
        $scope.state = 'chooseassignment';
        $scope.videoHasEnded = false;
    };
    $scope.vid = document.getElementById('videoControl');
    $scope.onChooseAssignment = function (assignment) {
        $scope.selectedAssignment = assignment;
        if (assignment.AssignmentType.Medias.length == 1) {
            $scope.onChooseMedia(assignment.AssignmentType.Medias[0]);
        }
    };
    $scope.onChooseMedia = function (media) {
        $scope.state = 'watchvideo'
        //TODO: put assignment.Medias[0].Url into video player
        $scope.vid.src = media.Url;
        $scope.vid.load();
    }

    $scope.vid.onended = function () {
        $scope.videoHasEnded = true;
        $scope.$apply();
    };
    $scope.vid.ontimeupdate = function (info) {
        status.innerHTML = $scope.vid.currentTime;
    };
    $scope.onRepeat = function () {
        //$("#VideoEndedButtons").hide();
        $scope.videoHasEnded = false;
        $scope.vid.play();
    };
    $scope.onReject = function () {
        //TODO: log reject response
        $scope.state = 'capturethanks';
    };
	$scope.onAgree = function(){
		$scope.state='capturesignature';
		if(!$scope.signaturePad){
            $scope.signaturePad = new SignaturePad(canvas);
		}
        if($scope.signaturePad){
            $scope.signaturePad.clear();
        }
	};
	$scope.onSigned = function(){
		$scope.signatureDataUrl = $scope.signaturePad.toDataURL();
		$scope.state='capturehealth';
	};
    $scope.run();

});
