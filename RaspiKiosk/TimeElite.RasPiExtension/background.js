"use strict";

let siteTab;
let errorTab;

function initTimer() {
	setInterval(function() {
		const code = "window.location.reload();";
		chrome.tabs.reload(siteTab.id);
	}, 10000);
}

function openTab(tab) {
	chrome.tabs.update(tab.id, { active: true });
}

chrome.tabs.onCreated.addListener(function(tab) {
	if (tab.url.includes("elite.int")) {
		siteTab = tab;
		initTimer();
	} else if (tab.url.includes("error.html")) {
		errorTab = tab;
	}
});

chrome.tabs.onUpdated.addListener(function (tabId, info) {
	if (tabId !== siteTab.id || info.status !== "complete") return;
	
	chrome.tabs.executeScript(tabId,
		{ code: "chrome.extension.onMessage.addListener(function(req, sender, respond) { if (req === 'Are you there?') { respond('Yes'); }});" }, 
		function() {
			var exec_error = setTimeout(function() { openTab(errorTab) }, 100);
			chrome.tabs.sendMessage(tabId, "Are you there?", function(yes_no) {
				if (yes_no === "Yes") {
					clearTimeout(exec_error);
					openTab(siteTab);
				}
			});
		}
	);
});