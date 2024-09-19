extends Node
var fn = null

func my_method():
	if fn:
		fn.call()
