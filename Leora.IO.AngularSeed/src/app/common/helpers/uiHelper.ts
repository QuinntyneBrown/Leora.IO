module app.common {

    "use strict";

    export class UIHelper implements IUIHelper {

        constructor(private $window: ng.IWindowService) {
            
        }

        public get currentFormFactor() {

            if (this.$window.screen.availWidth > 1) {
                return formFactor.phone;
            }

            return formFactor.desktop;
        }

        public clientRectEquals = (clientRectA: ClientRect, clientRectB: ClientRect) => {
            if (!clientRectA || !clientRectB) {
                return false;
            }
            return (clientRectA.top === clientRectB.top
                && clientRectA.left === clientRectB.left
                && clientRectA.bottom === clientRectB.bottom
                && clientRectA.right === clientRectB.right);
        }
        
        public getBoundingRectForDetachedElement = (detachedElement: HTMLElement) => {

            var clientRect: ClientRect;
            detachedElement.style.visibility = 'none';
            document.body.appendChild(detachedElement);
            clientRect = detachedElement.getBoundingClientRect();
            detachedElement.parentNode.removeChild(detachedElement);
            return clientRect;

        }

        public getSurroundingWindowSpace = (element: HTMLElement, _window: Window): IRect => {

            var clientRect = element.getBoundingClientRect();
            return {
                top: clientRect.top,
                left: clientRect.left,
                bottom: _window.innerHeight - clientRect.bottom,
                right: _window.innerWidth - clientRect.right
            };

        }

        public positionDetachedElement = (triggerElement: HTMLElement, element: HTMLElement, directionPriorityList: string[], elementRect: ClientRect, alignment: string, elementSurroundingWindowSpaceRect: IRect): IPositionDetachedElementResponseDto => {

            var triggerElementRect: ClientRect = triggerElement.getBoundingClientRect();

            if (alignment === "center") {

                var triggerElementVerticalMiddle = ((triggerElementRect.bottom - triggerElementRect.top) / 2) + triggerElementRect.top;

                var triggerElementHorizontalMiddle = ((triggerElementRect.right - triggerElementRect.left) / 2) + triggerElementRect.left;

                for (var i = 0; i < directionPriorityList.length; i++) {

                    var lastOption: boolean = directionPriorityList.length == i + 1;

                    switch (directionPriorityList[i]) {

                        case "top":

                            if (triggerElementRect.top > elementRect.height || lastOption) {

                                if (triggerElementRect.width > elementRect.width || lastOption) {
                                    element.style.top = (triggerElementRect.top - elementRect.height) + "px";
                                    element.style.left = triggerElementHorizontalMiddle - (elementRect.width / 2) + "px";
                                    return {
                                        position: directionPriorityList[i],
                                        elementRect: elementRect
                                    };

                                } else {
                                    var diff = (elementRect.width - triggerElementRect.width) / 2;

                                    if (((triggerElementRect.right + diff) < window.innerWidth) && triggerElementRect.left > diff) {
                                        element.style.top = (triggerElementRect.top - elementRect.height) + "px";
                                        element.style.left = triggerElementHorizontalMiddle - (elementRect.width / 2) + "px";
                                        return {
                                            position: directionPriorityList[i],
                                            elementRect: elementRect
                                        };
                                    }
                                }


                            }

                            break;

                        case "left":
                            if (triggerElementRect.left > elementRect.width || lastOption) {

                                if (triggerElementRect.height > elementRect.height || lastOption) {
                                    element.style.left = (triggerElementRect.left - elementRect.width) + "px";
                                    element.style.top = triggerElementVerticalMiddle - (elementRect.height / 2) + "px";
                                    return {
                                        position: directionPriorityList[i],
                                        elementRect: elementRect
                                    };
                                } else {
                                    var diff = (elementRect.height - triggerElementRect.height) / 2;

                                    if (((triggerElementRect.bottom + diff) < window.innerHeight) && triggerElementRect.top > diff) {
                                        element.style.left = (triggerElementRect.left - elementRect.width) + "px";
                                        element.style.top = triggerElementVerticalMiddle - (elementRect.height / 2) + "px";
                                        return {
                                            position: directionPriorityList[i],
                                            elementRect: elementRect
                                        };
                                    }
                                }
                            }


                            break;

                        case "bottom":

                            if (((window.innerHeight - triggerElementRect.bottom) > elementRect.height) || lastOption) {

                                if (triggerElementRect.width > elementRect.width || lastOption) {
                                    element.style.top = triggerElementRect.bottom + "px";
                                    element.style.left = triggerElementHorizontalMiddle - (elementRect.width / 2) + "px";
                                    return {
                                        position: directionPriorityList[i],
                                        elementRect: elementRect
                                    };

                                } else {

                                    var diff = (elementRect.width - triggerElementRect.width) / 2;

                                    if (((triggerElementRect.right + diff) < window.innerWidth) && triggerElementRect.left > diff) {
                                        element.style.top = triggerElementRect.bottom + "px";
                                        element.style.left = triggerElementHorizontalMiddle - (elementRect.width / 2) + "px";
                                        return {
                                            position: directionPriorityList[i],
                                            elementRect: elementRect
                                        };
                                    }
                                }
                            }

                            break;

                        case "right":


                            if (((window.innerWidth - triggerElementRect.right) > elementRect.width) || lastOption) {

                                if (triggerElementRect.height > elementRect.height || lastOption) {
                                    element.style.left = triggerElementRect.right + "px";
                                    element.style.top = triggerElementVerticalMiddle - (elementRect.height / 2) + "px";
                                    return {
                                        position: directionPriorityList[i],
                                        elementRect: elementRect
                                    };
                                } else {

                                    var diff = (elementRect.height - triggerElementRect.height) / 2;

                                    if (((triggerElementRect.bottom + diff) < window.innerHeight) && triggerElementRect.top > diff) {
                                        element.style.left = triggerElementRect.right + "px";
                                        element.style.top = triggerElementVerticalMiddle - (elementRect.height / 2) + "px";
                                        return {
                                            position: directionPriorityList[i],
                                            elementRect: elementRect
                                        };
                                    }
                                }
                            }

                            break;
                    }
                }

                throw new Error("Unable to position place pop up.");
            }

            if (alignment === "left") {
                element.style.left = triggerElementRect.left + "px";

                for (var i = 0; i < directionPriorityList.length; i++) {

                    var lastOption: boolean = directionPriorityList.length == i + 1;

                    if (directionPriorityList[i] === "top") {
                        if (triggerElementRect.top >= elementRect.height || lastOption) {

                            element.style.bottom = triggerElementRect.top + "px";

                            return {
                                position: directionPriorityList[i],
                                elementRect: elementRect
                            };
                        }
                    }

                    if (directionPriorityList[i] === "bottom") {
                        if (window.innerHeight - triggerElementRect.bottom >= elementRect.height || lastOption) {

                            element.style.top = triggerElementRect.bottom + "px";

                            return {
                                position: directionPriorityList[i],
                                elementRect: elementRect
                            };
                        }
                    }

                }
            }
        }

    }

    angular.module("app.common").service("uiHelper", ["$window",UIHelper]);
} 