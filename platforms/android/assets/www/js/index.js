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

var APIURL = (document.URL.match("^file") ? 'https://myvideotraining.azurewebsites.net/' : '/') + 'api/';

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

var getPointerEvent = function (event) {
    return event.originalEvent.targetTouches ? event.originalEvent.targetTouches[0] : event;
};

angular.module('app', []);
angular.module('app').controller('mainCtrl', function ($scope, $http, assignmentSvc) {
    $scope.status = 'ok so far';
    $scope.state = 'capturelogin';
    $scope.videoHasEnded = true;
    $scope.healthControl = $("#HealthSlider");
    $scope.healthValue = 50;
    $scope.healthIndicator = 50;
    $scope.currentSession = {};
    $scope.sessions = [];

    $scope.loadSessions = function () {
        if (typeof (Storage) != "undefined") {
            var ss = JSON.parse(localStorage.getItem("sessions"));
            if (ss) {
                $scope.sessions = ss;
            }
        }
    }
    $scope.saveSessions = function () {
        if (typeof (Storage) != "undefined") {
            localStorage.setItem("sessions", JSON.stringify($scope.sessions));
        }
        if($scope.sessions){
       		$scope.sessions.forEach( function(session){
       			if(!session.saved){
					assignmentSvc.post(session).then(
						function(response){
							if(response === 'OK'){
								session.saved = true;
								if (typeof (Storage) != "undefined") {
									localStorage.setItem("sessions", JSON.stringify($scope.sessions));
								}								
							}
						}
					);       			       				
       			}
       		});
		}

    }
    $scope.postSessions = function () {

    }
	
	$scope.saveCands = function(){
        if (typeof (Storage) != "undefined") {
            localStorage.setItem("candidates", JSON.stringify($scope.cands));
        }
	};
	$scope.loadCands = function(){
        if (typeof (Storage) != "undefined") {
            var cc = JSON.parse(localStorage.getItem("candidates"));
			if(cc){
				$scope.cands = cc;
				$scope.loadSessions();
				$scope.removeAgreedPatients();
			}
        }
	};
	
    $scope.onClearLocalData = function(){
    	$scope.sessions = [];
    	$scope.saveSessions();
    }


    $scope.run = function () {
		$scope.loadCands();
        $("#HealthSlider").on('touchstart mousedown mouseup mousemove', $scope.healthTap);
        $("#HealthIndicator").on('touchstart mousedown mouseup mousemove', $scope.healthTap);
        $scope.state = 'capturelogin';
    };


    $scope.login = function () {
        //TODO: actually check credentials
        $scope.state = 'choosepatient';
        $scope.getPatients();
    };
    $scope.getPatients = function () {
        $http.get(APIURL + "assignmentstodo").then(function (data) {
            $scope.cands = JSON.parse(data.data);
            //cands = [{ClientID, PersonName,Assignments:[{AssignmentID, AssignmentType { AssignmenttypeName}}]}]
        	$scope.loadSessions();
            $scope.removeAgreedPatients();
			$scope.saveCands();
        }, function(data,error){
			alert(JSON.stringify(data));
		});
    };

    $scope.removeAgreedPatients = function(){
        //remove any patients and their assignments if they have agreed
        var completedAssignments = [];
        var patientIndex = 0;
        var assignmentIndex = 0;
        var patient;
        if($scope.sessions){
			$scope.sessions.forEach(function (session) {
				if (session.response === "agreed") {
					completedAssignments.push(session.assignmentId);
					for (patientIndex = $scope.cands.length - 1; patientIndex > -1;patientIndex--)
					{
						patient = $scope.cands[patientIndex];
						for(assignmentIndex = patient.Assignments.length-1; assignmentIndex > -1; --assignmentIndex){
							if(patient.Assignments[assignmentIndex].AssignmentId === session.assignmentId){
								patient.Assignments.splice(assignmentIndex,1);
							}
						}
						if (patient.Assignments.length === 0) {
							$scope.cands.splice(patientIndex, 1);
						}
					}
				}
			});
        	
        }


    }

    $scope.onSelectPerson = function (person) {
        $scope.currentSession = {
            userEmail: 'nick@flikk.net',
            personId: person.PersonId,
            personName: person.PersonName
        };
        $scope.selectedPerson = person;
    }
    $scope.onChoosePatient = function () {
        $scope.state = 'chooseassignment';
        $scope.videoHasEnded = false;
    };
    $scope.vid = document.getElementById('videoControl');
    $scope.onChooseAssignment = function (assignment) {
        $scope.selectedAssignment = assignment;
        $scope.currentSession.assignmentId = assignment.AssignmentId;
        var itemToAdd = $scope.currentSession;
        $scope.sessions.forEach(function (ss) {
            if (ss === itemToAdd) {
                //its there dont need to add
                itemToAdd = null;
            }
        });
        if (itemToAdd) {
            $scope.sessions.push(itemToAdd);
        }
        $scope.state = 'watchvideo'
        //TODO: put assignment.Medias[0].Url into video player
        $scope.vid.src = assignment.AssignmentType.MediaUrl;
        $scope.vid.load();
    };

    $scope.vid.onended = function () {
        $scope.currentSession.videoHasEnded = true;
        $scope.currentSession.videoWatched= new Date().valueOf();
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
        $scope.currentSession.response = "not agreed";
        $scope.state = 'capturethanks';
    };
	$scope.onAgree = function(){

	    $scope.state = 'capturesignature';
		if(!$scope.signaturePad){
            $scope.signaturePad = new SignaturePad(canvas);
		}
        if($scope.signaturePad){
            $scope.signaturePad.clear();
        }
	};
	$scope.onSigned = function(){
	    $scope.currentSession.signatureDataUrl = $scope.signaturePad.toDataURL();
		$scope.state='capturehealth';
        $scope.healthValue = 50;
        setTimeout($scope.setHealthIndicator,500);
	};
	
	$scope.healthTap = function (e) {
	    e.preventDefault();
	    var pointer = getPointerEvent(e);
	    var target = document.getElementById("HealthSlider").getBoundingClientRect();
	    var indicator = document.getElementById("HealthIndicator").getBoundingClientRect();
	    $scope.healthValue = Math.floor(100 * (pointer.clientX - target.left) / target.width);
	    if ($scope.healthValue < 0) {
	        $scope.healthValue = 0;
	    } else if ($scope.healthValue > 100) {
	        $scope.healthValue = 100;
	    }
	    $scope.setHealthIndicator();
	};
	
	$scope.setHealthIndicator = function(){
		//position indicator in the correct position for healthValue

	    var target = document.getElementById("HealthSlider").getBoundingClientRect();
	    var indicator = document.getElementById("HealthIndicator").getBoundingClientRect();

	    $scope.healthIndicator = $scope.healthValue * target.width / 100  + target.left - indicator.width / 2;
	    $scope.$apply();
	}


	$scope.onHealthCompleted = function () {
	    $scope.currentSession.response = "agreed";
	    $scope.currentSession.healthValue = $scope.healthValue;
	    $scope.currentSession.completed = new Date().toISOString();
	    $scope.currentSession = null;
	    $scope.saveSessions();
	    $scope.removeAgreedPatients();
	    $scope.state = 'capturethanks';
	}

    $scope.run();

});
angular.module('app').factory('assignmentSvc',['$q','$http', function($q,$http){
    return {
        post: function (assignment) {
            var deferred = $q.defer();
            $http.post(APIURL + 'assignment',assignment)
            .success(function(data,status){
            	deferred.resolve('OK');
            })
            .error(function(data, status){
            	var msg = 'Could not post';
            	if(data.ExceptionMessage){
            		msg = data.ExceptionMessage;
            	}
            	deferred.resolve(msg);
            });
            return deferred.promise;
        }
    };
}]);
