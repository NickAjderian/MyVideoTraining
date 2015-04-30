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

function reset() {
    $("#PanelShowLogin").hide();
    $("#PanelChoosePatient").hide();
    $("#PanelChooseVideo").hide();
    $("#PanelWatchVideo").hide();
    $("#PanelSignature").hide();
    $("#PanelHealth").hide();
    $("#PanelThanks").hide();
}

$(function () {
    var vid = document.getElementById('videoControl');
    var status = document.getElementById('videoStatusDiv');
    vid.ontimeupdate = function (info) {
        status.innerHTML = vid.currentTime;
    };
    vid.onended = function (info) {
        status.innerHTML = "Finished";
    };

    
    $("#LoginButton").click(function () {
        $("#PanelShowLogin").hide();
        $("#PanelChoosePatient").show();
    });

    $("#SelectPatientButton").click(function () {
        $("#PanelChoosePatient").hide();
        $("#PanelChooseVideo").show();
    });

    $("#FireSafetyButton").click(function () {
        $("#PanelChooseVideo").hide();
        $("#PanelWatchVideo").show();
        $("#VideoEndedButtons").hide();
    });

    document.getElementById("videoControl").onended = function () {
        $("#VideoEndedButtons").show();
    };

    $("#AgreeButton").click(function () {
        $("#PanelWatchVideo").hide();
        $("#PanelSignature").show();
    });
    $("#RepeatButton").click(function () {
        $("#VideoEndedButtons").hide();
        document.getElementById("videoControl").play();
    });

    $("#SignatureCompleteButton").click(function () {
        $("#PanelSignature").hide();
        $("#PanelHealth").show();
    });

    $("#BackToSignatureButton").click(function () {
        $("#PanelSignature").show();
        $("#PanelHealth").hide();
    });

    $("#HealthCompleteButton").click(function () {
        $("#PanelThanks").show();
        $("#PanelHealth").hide();
    });

    $("#ThanksMessagePanel").click(function () {
        reset();
        $("#PanelShowLogin").show();
    });

    reset();

});