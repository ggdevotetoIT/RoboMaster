#pragma strict
private var originalIntensity : float;		//  Get intensity from light
private var newIntensity : float;			//  New intensity to be assigned to light
var minFlickerTime : float = 0.1;			//  Minimum time that intensity can change
var maxFlickerTime : float = 0.06;			//  Maximum time that intensity can change
var minIntensity : float = 0.4;				//  Minimum possible intensity multiplier
var maxIntensity : float = 1.0;				//  Maximum possible intensity multiplier

// Store the original color 
function Start () {
    originalIntensity = GetComponent(Light).intensity;
}

function Update () {
	GetComponent.<Light>().intensity = newIntensity;	
}

while (true) {
//		print ("Light Intensity = " + light.intensity);

		newIntensity = originalIntensity * (Random.Range(minIntensity, maxIntensity));

//		print ("New intensity = " + newIntensity);
//		print ("Original Intensity = " + originalIntensity);
//		print ("Blackfire FX");
        yield WaitForSeconds (Random.Range(minFlickerTime, maxFlickerTime));
}



