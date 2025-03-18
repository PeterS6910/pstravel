set -xe
rsync -avz --exclude=.git/   /c/w4/pstravel/portal/app/ root@usrv:/var/www/html/sources/pst_portal
ssh root@usrv "chown -R www-data:www-data /var/www/html/sources/pst_portal"
