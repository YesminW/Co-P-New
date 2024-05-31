import React from 'react';
import { Button, Container, Typography } from '@mui/material';
import { Link } from 'react-router-dom';
import Efooter from '../../Elements/Efooter';

import '../../assets/StyleSheets/EditProfileP.css';


export default function EditProfile() {
    return (
        <form className="edit-profile-container">
            <h2 style={{ textAlign: 'center', margin: 0, color: 'white', fontSize: '48px' }}> עריכת פרטים</h2>
            <Button className="btn" component={Link} to="/personal-details/or">
                פרטים אישיים אור
            </Button>
            <Button className="btn" component={Link} to="/personal-details/ronit-and-itan">
                פרטים אישיים רונית ואיתן
            </Button>
            {Efooter}
        </form>
    );
}
