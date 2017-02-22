// Archivo JScript

function OuterNode(Id) {

    this.Id = Id;
    this.Next = null;
    this.List = new OuterList();
}

function OuterList() {

    var startOuterList = null;
    
    this.insert = function insertOuterList(Id) {
    
        var newOuterNode = new OuterNode(Id);
        var aliasStart = startOuterList;
        
        // Iterates through the list:
        while(aliasStart != null && aliasStart.Next != null)
            aliasStart = aliasStart.Next;
            
        if(aliasStart == null)
            startOuterList = newOuterNode;
            
        else
            aliasStart.Next = newOuterNode;
    };
    
    this.deleteAll = function deleteOuterList() {
    
        while(startOuterList != null) {
        
            var tempStart = startOuterList;
            
            startOuterList = startOuterList.Next;
            tempStart.List.deleteAll();
            this.getDisplayContext().document.getElementById(tempStart.Id).style.display = "none";
            tempStart.List = null;
            tempStart = null;
        }
    }
    
    this.deleteElement = function deleteOuterNode(Id) {
    
        var previous = null;
        var current = startOuterList;

        while(current != null && current.Id != Id) {
        
            previous = current;
            current = current.Next;
        }
        
        // The target element is the first one:
        if(previous == null && current != null) {
        
            startOuterList = current.Next;
            current.List.deleteAll();
            this.getDisplayContext().document.getElementById(current.Id).style.display = "none";
            current.List = null;
            current = null;
        }
        else if(previous != null && current != null) {
        
            previous.Next = current.Next;
            current.List.deleteAll();
            this.getDisplayContext().document.getElementById(current.Id).style.display = "none";
            current.List = null;
            current = null; 
        }
    };
   
    this.findNode = function findOuterNode(Id) {
    
        var aliasStart = startOuterList;
        
        while(aliasStart != null && aliasStart.Id != Id) {
        
            var foundOuterNode = aliasStart.List.findNode(Id);
            
            if(foundOuterNode != null)
                return foundOuterNode;
            
            aliasStart = aliasStart.Next;
        }
            
        return aliasStart;
    };
    
    this.display = function displayOuterList() {
    
        var aliasStart = startOuterList;
        
        while(aliasStart != null) {
        
            if(this.getDisplayContext().document.getElementById(aliasStart.Id) != null) {
                this.getDisplayContext().document.getElementById(aliasStart.Id).style.display = "block";
                aliasStart.List.display();
            }
            
            aliasStart = aliasStart.Next;
        }
    };
    
    this.getDisplayContext = function getContainer() {
    
        //return parent.main.document.getElementById(currentContainerId);
        return parent.adentro.document.getElementById(currentContainerId);
    };
}

function initializeSA() {
    
    //debugger;
    if(parent.document.getElementById(mainFrameSetId).SA == null)
        parent.document.getElementById(mainFrameSetId).SA = new OuterList(); 
}

function findParent(currentElement, tagName) {

    var parentElement = currentElement.parentElement;
    
    while(parentElement != null && parentElement.tagName != tagName)
        parentElement = parentElement.parentElement;

    return parentElement;
}

function guardElement(Id) {

    //debugger;
    var objSA = parent.frameElement.SA;
    var objContainer = objSA.getDisplayContext();
    var parentRootNodeId = null;
    
    var parentElement = findParent(objContainer.document.getElementById(Id), "DIV");
   
    if(parentElement != null)
        parentRootNodeId = parentElement.Id;
   
    // The node is a rootNode:
    if(parentRootNodeId == null) {
    
        // The element does not exist, so insert it:
        if(objSA.findNode(Id) == null)
            objSA.insert(Id);
    }
    else {
    
        // The node is a child node or inner list node, so find the parent node:
        var foundOuterNode = objSA.findNode(parentRootNodeId);
        
        // Insert the child:
        if(foundOuterNode != null) {
        
            // The inner list node does not exist, so insert it:
            if(foundOuterNode.List.findNode(Id) == null)
                foundOuterNode.List.insert(Id);
        }
    }
}

function quitElement(Id) {

    //debugger;
    var objSA = parent.frameElement.SA;
    var objContainer = objSA.getDisplayContext();
    var parentRootNodeId = null;
    
    var parentElement = findParent(objContainer.document.getElementById(Id), "DIV");
   
    if(parentElement != null)
        parentRootNodeId = parentElement.Id;
   
    // The node is a rootNode:
    if(parentRootNodeId == null) {
      
        // The element exist, so delete it:
        if(objSA.findNode(Id) != null)
            objSA.deleteElement(Id);
    }
    else {
    
        // The node is a child node or inner list node, so find the parent node:
        var foundOuterNode = objSA.findNode(parentRootNodeId);
        
        // Look for the child:
        if(foundOuterNode != null) {
            foundOuterNode.List.deleteElement(Id);
        }
    }
}

function guardSearch(Id, addOrDelete) {
    
    // Add an element:
    if(addOrDelete > 0)
        guardElement(Id);
    
    else
        quitElement(Id);
}

function displaySearchSA() {

    //debugger;
    var url = adentro.location.href;
    var i = url.lastIndexOf("/");
    
    url = url.substring(i, url.lenght);
    url = url.replace(".", "_");
    url = url.replace("/","");
    url = url.replace("#","");
    
    for(var i=0; i<SABodyId.length; i++) {
    
        if(url.indexOf(SABodyId[i], 0) >= 0) {
    
            currentContainerId = SABodyId[i];
            
            var mainFrame = parent.document.getElementById(mainFrameSetId);
            mainFrame.SA.display(); 
        }              
    }  
}

var mainFrameSetId = 'adentro'
var currentContainerId = ''
//var SABodyId   = new Array("indsusa_htm", "indsusb_htm", "indsusc_htm", "indsusd_htm", "indsuse_htm", "indsusf_htm", "indsusg_htm",
//                            "indsush_htm", "indsusi_htm","indsusk_htm", "indsusl_htm", "indsusm_htm", "indsusn_htm", "indsuso_htm",
//                            "indsusp_htm", "indsusq_htm", "indsusr_htm", "indsuss_htm", "indsust_htm", "indsusu_htm", "indsusv_htm",
//                            "indsusy_htm","indsusz_htm");


var SABodyId = new Array("A-indsus_htm", "B-indsus_htm", "C-indsus_htm", "D-indsus_htm", "E-indsus_htm", "F-indsus_htm", "G-indsus_htm",
                            "H-indsus_htm", "I-indsus_htm", "J-indsus_htm", "K-indsus_htm", "L-indsus_htm", "M-indsus_htm", "N-indsus_htm",
                            "O-indsus_htm", "P-indsus_htm", "Q-indsus_htm", "R-indsus_htm", "S-indsus_htm", "T-indsus_htm", "U-indsus_htm",
                            "W-indsus_htm", "X-indsus_htm", "Y-indsus_htm", "Z-indsus_htm");

