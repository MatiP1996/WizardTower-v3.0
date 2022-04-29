#ifndef EDGEDETECTIONOUTLINESINCLUDE_INCLUDED
#define EDGEDETECTIONOUTLINESINCLUDE_INCLUDED


//sample points 
static float2 sobelSamplePoints[9] = 
{
    float2(-1,1), float2(0,1), float2(1,1),
    float2(-1,0), float2(0,0),float2(1,1),
    float2(-1,-1), float2(0,-1), float2(1,-1)      
};

// weights for the x axis edge detection
static float sobelX[9] =
{
    1, 2, 1,
    0, 0, 0,
    -1, -2, -1
};

//weights for the y axis edge detection
static float sobelY[9] =
{
    1, 0, -1,
    2, 0, -2,
    1, 0, -1
};


static float2 screenRes = float2(1920, 1080);


void DepthSobel_float(float2 UV, float sobelSampleMatrixRadius, out float _out)
{
    float2 sobel = 0;
    
    for (int i = 0; i < 9; i++) // for each sample point
    {
        float depth = SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV + sobelSamplePoints[i] / screenRes * sobelSampleMatrixRadius); // for each radius adjusted sample point, get the depth
        sobel += depth * float2(sobelX[i], sobelY[i]); // for each sample point, get the above calculated depth, multiply by sobel values, and total the values for each sample point
    }
    
    _out = sqrt(pow(sobel.x,2) + pow(sobel.y,2)); // output the magnitude of the sobel vector    
}

void ColourSobel_float(float2 UV, float sobelSampleMatrixRadius, out float Out)
{
    float2 sobelR = 0;
    float2 sobelG = 0;
    float2 sobelB = 0;

    for (int i = 0; i < 9; i++)
    {
        float3 rgb = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV + sobelSamplePoints[i] / screenRes * sobelSampleMatrixRadius);
        float2 kernel = float2(sobelX[i], sobelY[i]);

        sobelR += rgb.r * kernel;
        sobelG += rgb.g * kernel;
        sobelB += rgb.b * kernel;
    }

    //Out = max(length(sobelR), max(length(sobelG), length(sobelB)));
    Out = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV);

}




#endif