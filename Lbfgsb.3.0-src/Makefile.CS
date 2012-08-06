#
# Mac OS X settings.
# OSX Lion 64-bit
# Chunhua Shen, 29 Jul 2011.
#


MEXSUFFIX   = mexmaci64
MATLAB_HOME = /Applications/MATLAB_R2012a.app

MEX         = $(MATLAB_HOME)/bin/mex

CXX         = /opt/local/bin/g++-mp-4.4
# MacPorts version of gfortran
F77         = /opt/local/bin/gfortran-mp-4.4
CFLAGS      = -O3 -fPIC -fno-common -fexceptions
FFLAGS      = -O3 -x f77-cpp-input -fPIC -fno-common



TARGET = lbfgsb.$(MEXSUFFIX)
OBJS   = lbfgsb.o linpack.o timer.o blas.o

CFLAGS += -Wall -ansi -DMATLAB_MEXFILE


all: $(TARGET)

%.o: %.cpp
	$(CXX) $(CFLAGS) -I$(MATLAB_HOME)/extern/include -o $@ -c $^


%.o: %.f
	$(F77) $(FFLAGS) -o $@ -c $^


$(TARGET): $(OBJS)
	$(MEX) -cxx CXX=$(CXX) CC=$(CXX) FC=$(FCC) LD=$(CXX) -lgfortran  -lm \
        -O -output $@ $^

clean:
	rm -f *.o $(TARGET)


