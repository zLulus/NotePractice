// 雪花特效
(function () {
    var style = document.createElement("style");
    style.innerText = "body .snow{position: fixed;color: #fff;line-height: 1;text-shadow: 0 0 .2em #ffffff;z-index: 2;}";
    document.getElementsByTagName("head")[0].appendChild(style);

    var dpr = ~~document.documentElement.getAttribute("data-dpr") || 1;
    var wWidth = window.innerWidth;
    var wHeight = window.innerHeight;
    var maxNum = wWidth / 50;
    var snowArr = [];
    function createSnow(r) {
        var size = Math.random() + .8;
        var left = wWidth * Math.random();
        var speed = (Math.random() * .5 + .6) * size * dpr;
        var snow = document.createElement("div");
        snow.innerText = "❅";
        snow.className = "snow";
        var text = "";
        text += "font-size:";
        text += size;
        text += "em;left:";
        text += left;
        text += "px;bottom:100%;";
        snow.style.cssText = text;
        document.body.appendChild(snow);
        var top = r ? wHeight * Math.random() : (-snow.offsetHeight);
        snow.style.top = top + "px";
        snow.style.bottom = "auto";
        return {
            snow: snow,
            speed: speed,
            top: top
        }
    }
    function draw() {
        for (var i = 0; i < maxNum; i++) {
            if (!snowArr[i]) {
                if (typeof snowArr[i] == "undefined") {
                    snowArr[i] = createSnow(true);
                } else {
                    snowArr[i] = createSnow();
                }
            }
            var data = snowArr[i];
            data.top += data.speed;
            data.snow.style.top = data.top + "px";
            if (data.top > wHeight) {
                document.body.removeChild(data.snow);
                snowArr[i] = null;
            }
        }
        requestAnimationFrame(draw);
    }
    draw();
})();
